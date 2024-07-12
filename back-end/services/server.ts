import express, { Request, Response } from 'express';
import bodyParser from 'body-parser';
import axios from 'axios';
import cors from 'cors';

const app = express();
const PORT = process.env.PORT || 3005;

app.use(cors());
app.use(bodyParser.json());

const VERIFY_TOKEN = 'blchat'; // Defina o token de verificação aqui

// Rota para verificação do webhook
app.get('/webhook', (req: Request, res: Response) => {
  const mode = req.query['hub.mode'];
  const token = req.query['hub.verify_token'];
  const challenge = req.query['hub.challenge'];

  if (mode && token) {
    if (mode === 'subscribe' && token === VERIFY_TOKEN) {
      console.log('WEBHOOK_VERIFIED');
      res.status(200).send(challenge);
    } else {
      res.sendStatus(403);
    }
  } else {
    res.sendStatus(400);
  }
});

// Rota para receber mensagens do webhook
app.post('/webhook', (req: Request, res: Response) => {
  const body = req.body;
  console.log('Webhook received:', JSON.stringify(body, null, 2));
  res.status(200).send('EVENT_RECEIVED');
});

app.listen(PORT, () => {
  console.log(`Servidor rodando na porta ${PORT}`);
});
