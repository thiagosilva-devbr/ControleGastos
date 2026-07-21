import axios from "axios";

// Obtém a mensagem de erro retornada pela API
export function obterMensagemErro(
  erro: unknown,
  mensagemPadrao: string,
): string {

  // Verifica se o erro é do Axios
  if (axios.isAxiosError(erro)) {
    const mensagem = erro.response?.data?.mensagem;

    // Retorna a mensagem recebida da API
    if (typeof mensagem === "string") {
      return mensagem;
    }
  }

  // Retorna a mensagem padrão caso não exista uma mensagem da API
  return mensagemPadrao;
}