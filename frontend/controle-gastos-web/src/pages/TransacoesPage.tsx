import { useEffect, useMemo, useState } from "react";
import { pessoaService } from "../services/PessoaService";
import { transacaoService } from "../services/transacaoService";
import type { Pessoa } from "../types/pessoa";
import type {
  TipoTransacao,
  Transacao,
} from "../types/transacao";
import { obterMensagemErro } from "../utils/erros";
import { formatarMoeda } from "../utils/formatadores";

// Página responsável pelo cadastro e listagem das transações
export function TransacoesPage() {
  // Estados utilizados na página
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState<TipoTransacao>("Despesa");
  const [pessoaId, setPessoaId] = useState(0);
  const [erro, setErro] = useState("");
  const [carregando, setCarregando] = useState(false);

  // Obtém a pessoa selecionada
  const pessoaSelecionada = useMemo(
    () => pessoas.find((pessoa) => pessoa.id === pessoaId),
    [pessoas, pessoaId],
  );

  // Verifica se a pessoa selecionada é menor de idade
  const pessoaEhMenor =
    pessoaSelecionada !== undefined &&
    pessoaSelecionada.idade < 18;

  // Define automaticamente o tipo como despesa para menores de idade
  useEffect(() => {
    if (pessoaEhMenor) {
      setTipo("Despesa");
    }
  }, [pessoaEhMenor]);

  // Carrega as pessoas e as transações
  async function carregarDados() {
    try {
      const [listaPessoas, listaTransacoes] = await Promise.all([
        pessoaService.listar(),
        transacaoService.listar(),
      ]);

      setPessoas(listaPessoas);
      setTransacoes(listaTransacoes);

      // Seleciona a primeira pessoa da lista
      if (pessoaId === 0 && listaPessoas.length > 0) {
        setPessoaId(listaPessoas[0].id);
      }
    } catch (erro) {
      setErro(
        obterMensagemErro(
          erro,
          "Não foi possível carregar os dados.",
        ),
      );
    }
  }

  // Carrega os dados ao abrir a página
  useEffect(() => {
    void carregarDados();
  }, []);

  // Realiza o cadastro de uma nova transação
  async function cadastrar(event: React.FormEvent) {
    event.preventDefault();
    setErro("");

    const valorNumero = Number(valor.replace(",", "."));

    // Valida a descrição
    if (!descricao.trim()) {
      setErro("Informe a descrição.");
      return;
    }

    // Valida o valor informado
    if (!Number.isFinite(valorNumero) || valorNumero <= 0) {
      setErro("Informe um valor maior que zero.");
      return;
    }

    // Verifica se existe uma pessoa selecionada
    if (pessoaId <= 0) {
      setErro("Cadastre e selecione uma pessoa.");
      return;
    }

    try {
      setCarregando(true);

      // Envia os dados para a API
      await transacaoService.criar({
        descricao: descricao.trim(),
        valor: valorNumero,
        tipo,
        pessoaId,
      });

      // Limpa os campos do formulário
      setDescricao("");
      setValor("");
      setTipo("Despesa");

      // Atualiza a lista de transações
      await carregarDados();
    } catch (erro) {
      setErro(
        obterMensagemErro(
          erro,
          "Não foi possível cadastrar a transação.",
        ),
      );
    } finally {
      setCarregando(false);
    }
  }

  return (
    <section>
      <h1>Transações</h1>

      {/* Exibe um aviso caso não existam pessoas cadastradas */}
      {pessoas.length === 0 ? (
        <p className="mensagem aviso">
          Cadastre uma pessoa antes de registrar transações.
        </p>
      ) : (
        // Formulário de cadastro de transações
        <form onSubmit={cadastrar} className="form-grid">
          <label>
            Descrição
            <input
              value={descricao}
              maxLength={250}
              onChange={(event) => setDescricao(event.target.value)}
            />
          </label>

          <label>
            Valor
            <input
              type="number"
              min="0.01"
              step="0.01"
              value={valor}
              onChange={(event) => setValor(event.target.value)}
            />
          </label>

          <label>
            Pessoa
            <select
              value={pessoaId}
              onChange={(event) =>
                setPessoaId(Number(event.target.value))
              }
            >
              {pessoas.map((pessoa) => (
                <option key={pessoa.id} value={pessoa.id}>
                  {pessoa.nome} ({pessoa.idade} anos)
                </option>
              ))}
            </select>
          </label>

          <label>
            Tipo
            <select
              value={tipo}
              onChange={(event) =>
                setTipo(event.target.value as TipoTransacao)
              }
            >
              <option value="Despesa">Despesa</option>
              <option value="Receita" disabled={pessoaEhMenor}>
                Receita
              </option>
            </select>
          </label>

          {/* Informa que menores de idade só podem ter despesas */}
          {pessoaEhMenor && (
            <p className="mensagem aviso">
              Pessoas menores de 18 anos somente podem possuir
              despesas.
            </p>
          )}

          <button type="submit" disabled={carregando}>
            {carregando ? "Cadastrando..." : "Cadastrar"}
          </button>
        </form>
      )}

      {/* Exibe mensagens de erro */}
      {erro && <p className="mensagem erro">{erro}</p>}

      {/* Lista das transações cadastradas */}
      <table>
        <thead>
          <tr>
            <th>Código</th>
            <th>Descrição</th>
            <th>Pessoa</th>
            <th>Tipo</th>
            <th>Valor</th>
          </tr>
        </thead>

        <tbody>
          {transacoes.map((transacao) => (
            <tr key={transacao.id}>
              <td>{transacao.id}</td>
              <td>{transacao.descricao}</td>
              <td>{transacao.nomePessoa}</td>
              <td>{transacao.tipo}</td>
              <td>{formatarMoeda(transacao.valor)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  );
}