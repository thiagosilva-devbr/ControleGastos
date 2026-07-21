// Representa uma pessoa cadastrada no sistema
export interface Pessoa {
  id: number;
  nome: string;
  idade: number;
}

// Define os dados necessários para cadastrar uma nova pessoa
export interface CriarPessoaRequest {
  nome: string;
  idade: number;
}