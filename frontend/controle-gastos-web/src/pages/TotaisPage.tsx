import { useEffect, useState } from "react";
import { totaisService } from "../services/TotaisService";
import type { ConsultaTotais } from "../types/totais";
import { obterMensagemErro } from "../utils/erros";
import { formatarMoeda } from "../utils/formatadores";

// Página responsável por exibir os totais do sistema
export function TotaisPage() {
  // Estados da página
  const [totais, setTotais] = useState<ConsultaTotais | null>(null);
  const [erro, setErro] = useState("");

  // Carrega os totais ao abrir a página
  useEffect(() => {
    async function carregar() {
      try {
        // Busca os dados da API
        setTotais(await totaisService.consultar());
      } catch (erro) {
        setErro(
          obterMensagemErro(
            erro,
            "Não foi possível carregar os totais.",
          ),
        );
      }
    }

    void carregar();
  }, []);

  // Exibe a mensagem de erro
  if (erro) {
    return <p className="mensagem erro">{erro}</p>;
  }

  // Exibe a mensagem enquanto os dados são carregados
  if (!totais) {
    return <p>Carregando...</p>;
  }

  return (
    <section>
      <h1>Totais</h1>

      {/* Tabela com os totais de cada pessoa */}
      <table>
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Receitas</th>
            <th>Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>

        <tbody>
          {totais.pessoas.map((pessoa) => (
            <tr key={pessoa.pessoaId}>
              <td>{pessoa.nome}</td>
              <td>{formatarMoeda(pessoa.totalReceitas)}</td>
              <td>{formatarMoeda(pessoa.totalDespesas)}</td>
              <td>{formatarMoeda(pessoa.saldo)}</td>
            </tr>
          ))}
        </tbody>

        {/* Exibe os totais gerais do sistema */}
        <tfoot>
          <tr>
            <th>Total geral</th>
            <th>{formatarMoeda(totais.totalGeral.totalReceitas)}</th>
            <th>{formatarMoeda(totais.totalGeral.totalDespesas)}</th>
            <th>{formatarMoeda(totais.totalGeral.saldo)}</th>
          </tr>
        </tfoot>
      </table>
    </section>
  );
}