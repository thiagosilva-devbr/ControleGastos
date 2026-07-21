// Representa os totais de uma pessoa
export interface TotalPessoa {
  pessoaId: number;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

// Representa os totais gerais do sistema
export interface TotalGeral {
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

// Agrupa os totais por pessoa e o total geral
export interface ConsultaTotais {
  pessoas: TotalPessoa[];
  totalGeral: TotalGeral;
}