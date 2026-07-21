import api from "./api";
import type { ConsultaTotais } from "../types/totais";

// Serviço responsável por consultar os totais do sistema
export const totaisService = {

  // Busca os totais de receitas, despesas e saldo
  async consultar(): Promise<ConsultaTotais> {
    const response = await api.get<ConsultaTotais>("/totais");

    // Retorna os dados recebidos da API
    return response.data;
  },
};