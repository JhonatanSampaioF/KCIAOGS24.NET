import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { FiSend, FiSmile, FiPaperclip, FiMic } from 'react-icons/fi';
import { AiOutlineThunderbolt } from 'react-icons/ai'; // Ícone de raio
import Header from '../components/header';
import backgroundImage from '../assets/image.png'; // Ajuste o caminho conforme necessário
import { io } from 'socket.io-client';

const socket = io('https://tetochat-8m0r.onrender.com'); // Ajuste para o endereço correto do seu servidor

const Chat = () => {
  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState('');
  const [contacts, setContacts] = useState([]);
  const [selectedContact, setSelectedContact] = useState(null);
  const [sending, setSending] = useState(false); // Adicionado para evitar envios múltiplos

  useEffect(() => {
    const fetchContacts = async () => {
      try {
        const response = await axios.get('https://tetochat-8m0r.onrender.com/contacts', {
          withCredentials: true
        });
        setContacts(response.data);
      } catch (error) {
        console.error('Erro ao buscar contatos:', error);
      }
    };

    fetchContacts();
  }, []);

  useEffect(() => {
    if (selectedContact) {
      const fetchMessages = async () => {
        try {
          const response = await axios.get(`https://tetochat-8m0r.onrender.com/messages?contact=${selectedContact.id}`);
          setMessages(response.data);
        } catch (error) {
          console.error('Erro ao buscar mensagens:', error);
        }
      };

      fetchMessages();
    }
  }, [selectedContact]);

  useEffect(() => {
    socket.on('new_message', (message) => {
      if (message.contact_id === selectedContact?.id) {
        setMessages((prevMessages) => [...prevMessages, message]);
      }
    });

    return () => {
      socket.off('new_message');
    };
  }, [selectedContact]);

  const handleSendMessage = async () => {
    if (selectedContact && newMessage.trim() !== '' && !sending) { // Verificação adicionada
      setSending(true);
      try {
        const response = await axios.post('https://tetochat-8m0r.onrender.com/send', {
          toPhone: selectedContact.phone,
          text: newMessage,
        });

        if (response.status === 200) {
          setNewMessage('');
          console.log('Mensagem enviada com sucesso');
        }
      } catch (error) {
        console.error('Erro ao enviar mensagem:', error);
      } finally {
        setSending(false);
      }
    }
  };

  const handleKeyPress = (event) => {
    if (event.key === 'Enter') {
      handleSendMessage();
    }
  };

  return (
    <div className="flex flex-col h-screen">
      <Header />
      <div className="flex flex-grow overflow-hidden">
        {/* Coluna esquerda com 40% da largura */}
        <div className="flex-shrink-0 w-1/4 bg-white border-r border-gray-200 flex flex-col">
          <input
            type="text"
            placeholder="Pesquise por nome ou número"
            className="w-full p-2 border-b border-gray-200"
          />
          <div className="flex-grow p-2 overflow-y-auto">
            <ul>
              {contacts.map((contact) => (
                <li
                  key={contact.id}
                  className="flex items-center p-2 cursor-pointer hover:bg-gray-100"
                  onClick={() => setSelectedContact(contact)}
                >
                  <img src={contact.profilePic} alt={contact.name} className="w-10 h-10 rounded-full mr-2" />
                  <div>
                    <div className="font-bold">{contact.name}</div>
                    <div className="text-sm text-gray-600">{contact.lastMessage}</div>
                  </div>
                </li>
              ))}
            </ul>
          </div>
        </div>
        {/* Coluna direita com 60% da largura */}
        <div className="flex-grow flex flex-col" style={{ backgroundImage: `url(${backgroundImage})`, backgroundSize: 'cover' }}>
          {/* Div para exibir o nome do contato */}
          {selectedContact && (
            <div className="w-full p-2 bg-white border-b border-gray-200">
              <div className="text-lg ml-10 font-bold">{selectedContact.name}</div>
            </div>
          )}
          {/* Div para exibir as mensagens */}
          <div className="flex-grow p-4 overflow-y-auto">
            {messages.map((message) => (
              <div key={message.id} className={`max-w-xs p-3 my-2 rounded-lg ${message.from_phone === 'me' || message.contact_name === 'API' ? 'ml-auto bg-green-200 text-black' : 'mr-auto bg-blue-200 text-black'}`}>
                {message.message_body}
              </div>
            ))}
          </div>
          {/* Div para enviar nova mensagem */}
          <div className="flex items-center p-4 bg-white border-t border-gray-200">
            <button className="p-2 text-gray-500">
              <FiSmile size={24} />
            </button>
            <button className="p-2 text-gray-500">
              <AiOutlineThunderbolt size={24} />
            </button>
            <button className="p-2 text-gray-500">
              <FiPaperclip size={24} />
            </button>
            <input
              type="text"
              value={newMessage}
              onChange={(e) => setNewMessage(e.target.value)}
              onKeyPress={handleKeyPress} // Adicionado evento de tecla
              placeholder="Digite uma mensagem"
              className="flex-grow p-2 mx-2 border rounded-full"
            />
            <button className="p-2 text-gray-500">
              <FiMic size={24} />
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Chat;
