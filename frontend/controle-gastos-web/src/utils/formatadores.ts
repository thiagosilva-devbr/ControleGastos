// Formata um valor para o padrão de moeda brasileira
export function formatarMoeda(valor: number): string {

  // Retorna o valor formatado em Real (R$)
  return new Intl.NumberFormat("pt-BR", {
    style: "currency",
    currency: "BRL",
  }).format(valor);
}