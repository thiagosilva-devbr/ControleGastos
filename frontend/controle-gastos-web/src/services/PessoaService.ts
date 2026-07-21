import api from "./api";
import type { CriarPessoaRequest, Pessoa } from "../types/pessoa";

// Serviço responsável pelas operações relacionadas às pessoas
export const pessoaService = {

  // Busca todas as pessoas cadastradas
  async listar(): Promise<Pessoa[]> {
    const response = await api.get("/pessoas");

    // Exibe os dados recebidos no console
    console.log(response.data);

    // Retorna a lista de pessoas
    return response.data;
  },

  // Cadastra uma nova pessoa
  async criar(dados: CriarPessoaRequest): Promise<Pessoa> {
    const response = await api.post<Pessoa>("/pessoas", dados);

    // Retorna a pessoa cadastrada
    return response.data;
  },

  // Exclui uma pessoa pelo código
  async excluir(id: number): Promise<void> {
    await api.delete(`/pessoas/${id}`);
  },
};