import api from "./api";
import type {
  CriarTransacaoRequest,
  Transacao,
} from "../types/transacao";

// Serviço responsável pelas operações relacionadas às transações
export const transacaoService = {

  // Busca todas as transações cadastradas
  async listar(): Promise<Transacao[]> {
    const response = await api.get<Transacao[]>("/transacoes");

    // Retorna a lista de transações
    return response.data;
  },

  // Cadastra uma nova transação
  async criar(dados: CriarTransacaoRequest): Promise<Transacao> {
    const response = await api.post<Transacao>(
      "/transacoes",
      dados,
    );

    // Retorna a transação cadastrada
    return response.data;
  },
};