import { useEffect, useState } from "react";
import type { Pessoa } from "../types/pessoa";
import { obterMensagemErro } from "../utils/erros";
import { pessoaService } from "../services/PessoaService";

// Página responsável pelo cadastro, listagem e exclusão de pessoas
export function PessoasPage() {
  // Estados utilizados pela página
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [erro, setErro] = useState("");
  const [carregando, setCarregando] = useState(false);

  // Carrega a lista de pessoas cadastradas
  async function carregarPessoas() {
    try {
      const dados = await pessoaService.listar();
      setPessoas(dados);
    } catch (erro) {
      setErro(
        obterMensagemErro(
          erro,
          "Não foi possível carregar as pessoas.",
        ),
      );
    }
  }

  // Executa o carregamento ao abrir a página
  useEffect(() => {
    void carregarPessoas();
  }, []);

  // Realiza o cadastro de uma nova pessoa
  async function cadastrar(event: React.FormEvent) {
    event.preventDefault();
    setErro("");

    const idadeNumero = Number(idade);

    // Valida o nome informado
    if (!nome.trim()) {
      setErro("Informe o nome.");
      return;
    }

    // Valida a idade informada
    if (
      !Number.isInteger(idadeNumero) ||
      idadeNumero < 0 ||
      idadeNumero > 150
    ) {
      setErro("Informe uma idade entre 0 e 150.");
      return;
    }

    try {
      setCarregando(true);

      // Envia os dados para a API
      await pessoaService.criar({
        nome: nome.trim(),
        idade: idadeNumero,
      });

      // Limpa o formulário
      setNome("");
      setIdade("");

      // Atualiza a lista de pessoas
      await carregarPessoas();
    } catch (erro) {
      setErro(
        obterMensagemErro(
          erro,
          "Não foi possível cadastrar a pessoa.",
        ),
      );
    } finally {
      setCarregando(false);
    }
  }

  // Exclui uma pessoa cadastrada
  async function excluir(pessoa: Pessoa) {
    const confirmou = window.confirm(
      `Excluir ${pessoa.nome}? Todas as transações ` +
      "dessa pessoa também serão apagadas.",
    );

    if (!confirmou) return;

    try {
      await pessoaService.excluir(pessoa.id);

      // Atualiza a lista após a exclusão
      await carregarPessoas();
    } catch (erro) {
      setErro(
        obterMensagemErro(
          erro,
          "Não foi possível excluir a pessoa.",
        ),
      );
    }
  }

  return (
    <section>
      <h1>Pessoas</h1>

      {/* Formulário de cadastro */}
      <form onSubmit={cadastrar} className="form-grid">
        <label>
          Nome
          <input
            value={nome}
            maxLength={150}
            onChange={(event) => setNome(event.target.value)}
          />
        </label>

        <label>
          Idade
          <input
            type="number"
            min="0"
            max="150"
            value={idade}
            onChange={(event) => setIdade(event.target.value)}
          />
        </label>

        <button type="submit" disabled={carregando}>
          {carregando ? "Cadastrando..." : "Cadastrar"}
        </button>
      </form>

      {/* Exibe mensagens de erro */}
      {erro && <p className="mensagem erro">{erro}</p>}

      {/* Lista das pessoas cadastradas */}
      <table>
        <thead>
          <tr>
            <th>Código</th>
            <th>Nome</th>
            <th>Idade</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.map((pessoa) => (
            <tr key={pessoa.id}>
              <td>{pessoa.id}</td>
              <td>{pessoa.nome}</td>
              <td>{pessoa.idade}</td>
              <td>
                <button
                  type="button"
                  className="perigo"
                  onClick={() => void excluir(pessoa)}
                >
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  );
}