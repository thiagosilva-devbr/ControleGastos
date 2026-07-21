namespace ControleGastos.Modelos;

/// <summary>
/// Representa uma pessoa cadastrada no sistema.
/// </summary>
public class PessoaEntidade
{
    /// <summary>Identificador gerado automaticamente.</summary>
    public int Id { get; set; }

    /// <summary>Nome da pessoa.</summary>
    public required string Nome { get; set; }

    /// <summary>Idade usada na validação das transações.</summary>
    public int Idade { get; set; }

    /// <summary>Transações pertencentes à pessoa.</summary>
    public ICollection<TransacaoEntidade> TransacaoEntidade { get; set; } = [];
}
