import pool from "../models/db.js";
import axios from "axios";
import multer from 'multer';
import path from 'path';


async function sendMessage(toPhone, text, whatsappBusinessAccountId, socket) {
  const url = `https://graph.facebook.com/v20.0/${whatsappBusinessAccountId}/messages`;
  const data = {
    messaging_product: "whatsapp",
    recipient_type: "individual",
    to: toPhone,
    type: "text",
    text: { body: text },
  };
  console.log("Iniciando envio de mensagem para o WhatsApp");

  const headers = {
    Authorization: `Bearer ${process.env.WHATSAPP_ACCESS_TOKEN}`,
  };

  try {
    const response = await axios.post(url, data, { headers });

    console.log(`Mensagem enviada para o WhatsApp: ${text} para ${toPhone}`);

    if (socket) {
      socket.emit("new_message", {
        phone_number_id: whatsappBusinessAccountId,
        to: toPhone,
        message_body: text,
        timestamp: new Date().getTime(),
      });
    }

    return response.data;
  } catch (error) {
    console.error("Erro ao enviar mensagem para o WhatsApp:", error);
  }
}

// Buscar mensagens existentes
export const getMessages = async (req, res) => {
  try {
    const [rows] = await pool.query("SELECT * FROM whatsapp_messages");
    res.json(rows);
  } catch (error) {
    console.error("Erro ao buscar mensagens:", error);
    res.status(500).send("Erro ao buscar mensagens");
  }
};

// Enviar mensagem
export const send = async (req, res) => {
  const { toPhone, text } = req.body;

  // Verifica se o usuário está autenticado
  if (!req.user || !req.user.id) {
    return res.status(401).send("Usuário não autenticado");
  }
  const userId = req.user.id;

  // Verifica se os campos toPhone e text foram enviados
  if (!toPhone || !text) {
    return res.status(400).send("toPhone e text são obrigatórios");
  }

  // Verifica se as variáveis de ambiente do WhatsApp estão configuradas
  if (
    !process.env.WHATSAPP_BUSINESS_ACCOUNT_ID ||
    !process.env.DISPLAY_PHONE_NUMBER
  ) {
    return res.status(500).send("Configurações do WhatsApp estão ausentes");
  }

  try {
    // Envia a mensagem para o WhatsApp
    await sendMessage(toPhone, text, process.env.WHATSAPP_BUSINESS_ACCOUNT_ID);

    // Verifica se o contato já existe no banco de dados, senão, insere
    const [contactRows] = await pool.query(
      "SELECT id FROM contacts WHERE phone = ?",
      [toPhone]
    );
    let contactId;
    if (contactRows.length > 0) {
      contactId = contactRows[0].id;
    } else {
      const [result] = await pool.query(
        "INSERT INTO contacts (name, phone) VALUES (?, ?)",
        ["API", toPhone]
      );
      contactId = result.insertId;
    }

    // Cria um ID de conversa único
    const conversationId = `conv-${Date.now()}`;

    // Insere a mensagem no banco de dados
    const insertMessageQuery = `
      INSERT INTO whatsapp_messages (phone_number_id, display_phone_number, contact_name, wa_id, message_id, message_from, message_timestamp, message_type, message_body, contact_id, user_id)
      VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
    `;
    await pool.query(insertMessageQuery, [
      process.env.WHATSAPP_BUSINESS_ACCOUNT_ID,
      process.env.DISPLAY_PHONE_NUMBER,
      "API",
      toPhone,
      `msg-${Date.now()}`,
      "me",
      Math.floor(Date.now() / 1000).toString(),
      "text",
      text,
      contactId,
      userId,
    ]);

    // Seleciona o departamento do usuário
    const [userRow] = await pool.query(
      "SELECT department_id FROM users WHERE id = ?",
      [userId]
    );
    const [departamentRowName] = await pool.query(
      "SELECT name FROM departments WHERE id = ?",
      [userRow[0].department_id]
    );

    const departmentName = departamentRowName[0].name;
    console.log("Departamento encontrado:", departmentName);

    // Adicionar a mensagem na fila do departamento correspondente
    const queueTableName = `queueOf${departmentName}`;
    const insertQueueQuery = `INSERT INTO ${queueTableName} (contact_id, conversation_id, status)
      VALUES (?, ?, 'fila')`;
    await pool.query(insertQueueQuery, [contactId, conversationId]);
    console.log("Mensagem adicionada à fila:", queueTableName);

    // Responde com sucesso
    res.status(200).send("Mensagem enviada e salva com sucesso");
  } catch (error) {
    console.error("Erro ao enviar mensagem:", error);
    res.status(500).send("Erro ao enviar mensagem");
  }
};

// Webhook para verificar assinatura
export const getWebhook = function (req, res) {
  if (
    req.query["hub.mode"] == "subscribe" &&
    req.query["hub.verify_token"] == process.env.WEBHOOK_VERIFY_TOKEN
  ) {
    res.send(req.query["hub.challenge"]);
  } else {
    res.sendStatus(400);
  }
};

// Receber mensagem (Webhook)
export const receiveMessage = async (request, response) => {
  console.log("Incoming webhook: " + JSON.stringify(request.body));

  const entries = request.body.entry;

  if (entries && entries.length > 0) {
    let allEntriesProcessed = true;

    for (const entry of entries) {
      const changes = entry.changes;
      for (const change of changes) {
        const data = change.value;
        if (data && data.messages && data.messages.length > 0) {
          const message = data.messages[0];
          const contact =
            data.contacts && data.contacts.length > 0 ? data.contacts[0] : null;

          // Logar o ID da mensagem e do contato
          console.log(`Processando mensagem com ID: ${message.id} de ${contact.wa_id}`);

          // Verificar se a mensagem já foi processada
          const [messageExists] = await pool.query(
            "SELECT id FROM whatsapp_messages WHERE message_id = ?",
            [message.id]
          );

          if (messageExists.length > 0) {
            console.log(`Mensagem já processada com ID: ${message.id}`);
            continue; // Ignora esta mensagem
          }

          if (
            !contact ||
            !contact.profile ||
            !contact.wa_id ||
            !message ||
            !message.text ||
            !message.text.body
          ) {
            console.error(
              "Dados inválidos recebidos:",
              JSON.stringify(request.body)
            );
            allEntriesProcessed = false;
            continue;
          }

          let contactId;
          let isNewContact = false;
          let contactStatus = null; // Adiciona uma variável para verificar o status

          try {
            const [contactRows] = await pool.query(
              "SELECT id, status FROM contacts WHERE phone = ?",
              [contact.wa_id]
            );
            if (contactRows.length > 0) {
              contactId = contactRows[0].id;
              contactStatus = contactRows[0].status; // Obtem o status do contato
            } else {
              const [result] = await pool.query(
                "INSERT INTO contacts (name, phone, status) VALUES (?, ?, 'novo')", // Insere o contato como 'novo'
                [contact.profile.name, contact.wa_id]
              );
              contactId = result.insertId;
              isNewContact = true;
            }
          } catch (err) {
            console.error("Erro ao buscar ou criar contato:", err);
            allEntriesProcessed = false;
            continue;
          }

          // Se o contato for novo, envia a mensagem de boas-vindas e atualiza o status
          if (isNewContact || contactStatus === "novo") {
            const initialBotMessage = `Olá ${contact.profile.name}! Seja muito bem-vindo(a) ao atendimento digital da Teto Bello. Para direcioná-lo, selecione uma opção abaixo:\n\n1 - Comercial / Vendas\n2 - Instalação / Assistência Técnica\n3 - Financeiro / Adm\n4 - Projetos\n5 - Compras\n6 - Trabalhe Conosco`;

            await sendMessage(
              contact.wa_id,
              initialBotMessage,
              process.env.WHATSAPP_BUSINESS_ACCOUNT_ID
            );

            // Atualiza o status do contato para "aguardando_resposta"
            await pool.query(
              "UPDATE contacts SET status = 'aguardando_resposta' WHERE id = ?",
              [contactId]
            );
          } else if (contactStatus === "aguardando_resposta") {
            // Resposta do usuário
            const userResponse = message.text.body.trim();

            switch (userResponse) {
              case "1": // Comercial / Vendas
                await sendMessage(
                  contact.wa_id,
                  `Seja muito bem-vindo(a) à Teto Bello! Vamos começar! Qual produto você procura?\n1 - Envidraçamento de sacadas/complementos\n2 - Coberturas\n3 - Cobertura com envidraçamento de sacadas\n4 - Vidraçaria (Vidros/Box/Espelhos)\n5 - Esquadrias de alumínio\n6 - Guarda corpo e corrimão\n7 - Fachadas\n8 - Cortinas e persianas\n9 - Manutenção\n10 - Mais de um item acima.`,
                  process.env.WHATSAPP_BUSINESS_ACCOUNT_ID
                );

                // Atualiza o status para evitar o loop
                await pool.query(
                  "UPDATE contacts SET status = 'orcamentos' WHERE id = ?",
                  [contactId]
                );
                break;

              case "2": // Instalação / Assistência Técnica
                await sendMessage(
                  contact.wa_id,
                  `O que deseja?\n1 - Saber o prazo de instalação do meu contrato\n2 - Agendar Instalação\n3 - Solicitar Assistência Técnica.\nPor favor, informe seu nome, nome do condomínio, número do apartamento e número do contrato.`,
                  process.env.WHATSAPP_BUSINESS_ACCOUNT_ID
                );

                // Atualiza o status para evitar o loop
                await pool.query(
                  "UPDATE contacts SET status = 'instalacao' WHERE id = ?",
                  [contactId]
                );
                break;

              case "3": // Financeiro / Adm
                await sendMessage(
                  contact.wa_id,
                  `O que deseja?\n1 - Solicitar Boleto Bancário\n2 - Informações financeiras referentes ao meu contrato.\nPor favor, informe seu nome, nome do condomínio, número do apartamento e número do contrato.`,
                  process.env.WHATSAPP_BUSINESS_ACCOUNT_ID
                );

                // Atualiza o status para evitar o loop
                await pool.query(
                  "UPDATE contacts SET status = 'financeiro' WHERE id = ?",
                  [contactId]
                );
                break;

              case "4": // Projetos
                await sendMessage(
                  contact.wa_id,
                  `Você ainda não recebeu seu projeto executivo? Entre em contato com seu consultor técnico para dar continuidade ao atendimento. Caso precise de suporte, informe seu nome, condomínio, apartamento e número do contrato, e redirecionaremos seu atendimento.`,
                  process.env.WHATSAPP_BUSINESS_ACCOUNT_ID
                );

                // Atualiza o status para evitar o loop
                await pool.query(
                  "UPDATE contacts SET status = 'projetos' WHERE id = ?",
                  [contactId]
                );
                break;

              case "5": // Compras
                await sendMessage(
                  contact.wa_id,
                  `Deseja vender para nós? Por favor, envie seu portfólio abaixo. Caso já seja fornecedor, informe seu nome, o nome do comprador e a empresa para contato.`,
                  process.env.WHATSAPP_BUSINESS_ACCOUNT_ID
                );

                // Atualiza o status para evitar o loop
                await pool.query(
                  "UPDATE contacts SET status = 'compras' WHERE id = ?",
                  [contactId]
                );
                break;

              case "6": // Trabalhe Conosco
                await sendMessage(
                  contact.wa_id,
                  `Envie seu currículo atualizado abaixo. Se houver uma vaga disponível que corresponda ao seu perfil, entraremos em contato.`,
                  process.env.WHATSAPP_BUSINESS_ACCOUNT_ID
                );

                // Atualiza o status para evitar o loop
                await pool.query(
                  "UPDATE contacts SET status = 'trabalhe_conosco' WHERE id = ?",
                  [contactId]
                );
                break;

              default:
                await sendMessage(
                  contact.wa_id,
                  "Opção inválida. Por favor, selecione uma das opções válidas.",
                  process.env.WHATSAPP_BUSINESS_ACCOUNT_ID
                );
                break;
            }
          }

          // Insere a mensagem recebida no banco de dados
          const sql =
            "INSERT INTO whatsapp_messages (phone_number_id, display_phone_number, contact_name, wa_id, message_id, message_from, message_timestamp, message_type, message_body, contact_id) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
          const values = [
            data.metadata.phone_number_id,
            data.metadata.display_phone_number,
            contact.profile.name,
            contact.wa_id,
            message.id,
            message.from,
            message.timestamp,
            message.type,
            message.text.body,
            contactId,
          ];

          try {
            await pool.query(sql, values);
            console.log(`Mensagem inserida no banco de dados com ID: ${message.id}`);

            // Emite um evento para os clientes conectados via Socket.IO
            global.io.emit("new_message", {
              phone_number_id: data.metadata.phone_number_id,
              display_phone_number: data.metadata.display_phone_number,
              contact_name: contact.profile.name,
              wa_id: contact.wa_id,
              message_id: message.id,
              message_from: message.from,
              message_timestamp: message.timestamp,
              message_type: message.type,
              message_body: message.text.body,
              contact_id: contactId,
            });

          } catch (err) {
            console.error("Erro ao inserir dados no banco de dados:", err);
            allEntriesProcessed = false;
          }
        }
      }
    }

    if (allEntriesProcessed) {
      response.sendStatus(200);
    } else {
      response.sendStatus(500);
    }
  } else {
    console.error(
      "Estrutura do webhook não corresponde ao esperado:",
      JSON.stringify(request.body)
    );
    response.sendStatus(400);
  }
};

// Configuração do multer para armazenar arquivo na memória
const storage = multer.memoryStorage();
export const upload = multer({ storage });

// Função para enviar e salvar um arquivo no banco de dados
// Controller function to handle file sending
export const sendFile = async (req, res) => {
  try {
    // Check if a file is included in the request
    if (!req.file) {
      return res.status(400).json({ message: "File is required." });
    }

    const fileBuffer = req.file.buffer; // Buffer of the uploaded file
    const { toPhone, whatsappBusinessAccountId, fileType } = req.body;

    // Replace this with actual logic to send file to WhatsApp API or other processing
    const url = `https://graph.facebook.com/v20.0/${whatsappBusinessAccountId}/messages`;
    const headers = {
      Authorization: `Bearer ${process.env.WHATSAPP_ACCESS_TOKEN}`,
      'Content-Type': 'application/json',
    };

    const data = {
      messaging_product: 'whatsapp',
      recipient_type: 'individual',
      to: toPhone,
      type: fileType, // e.g., 'image', 'audio'
      [fileType]: { link: `data:${req.file.mimetype};base64,${fileBuffer.toString('base64')}` },
    };

    const response = await axios.post(url, data, { headers });
    res.status(200).json({ message: "File sent successfully", data: response.data });
  } catch (error) {
    console.error("Error in sendFile:", error);
    res.status(500).json({ message: "Error sending file" });
  }
};

// Função para recuperar um arquivo do banco de dados
export async function getFile(req, res) {
  const { messageId } = req.params;

  try {
    const [rows] = await pool.query(
      'SELECT file_type, file_data, file_name FROM media_files WHERE message_id = ?',
      [messageId]
    );

    if (rows.length === 0) {
      return res.status(404).json({ error: 'Arquivo não encontrado' });
    }

    const file = rows[0];
    
    // Configura o header para o tipo correto de arquivo e envia o conteúdo
    const mimeType = {
      image: 'image/jpeg',
      audio: 'audio/mpeg',
      video: 'video/mp4',
      document: 'application/octet-stream' // Genérico para documentos
    }[file.file_type] || 'application/octet-stream';

    res.set('Content-Type', mimeType);
    res.set('Content-Disposition', `attachment; filename="${file.file_name}"`);
    res.send(file.file_data);
  } catch (error) {
    console.error("Erro ao recuperar arquivo:", error);
    res.status(500).json({ error: 'Erro ao recuperar arquivo' });
  }
}