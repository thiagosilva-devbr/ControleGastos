import axios from "axios";

// Cria a configuração padrão para comunicação com a API
const api = axios.create({
  // Define a URL base da API
  baseURL: import.meta.env.VITE_API_URL,

  // Define o tipo de conteúdo das requisições
  headers: {
    "Content-Type": "application/json",
  },
});

// Exporta a configuração para ser utilizada nos serviços
export default api;