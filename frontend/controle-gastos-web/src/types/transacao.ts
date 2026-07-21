// Define os tipos de transação permitidos no sistema
export type TipoTransacao = "Despesa" | "Receita";

// Representa uma transação cadastrada
export interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  pessoaId: number;
  nomePessoa: string;
}

// Define os dados necessários para cadastrar uma nova transação
export interface CriarTransacaoRequest {
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  pessoaId: number;
}