using ControleGastos.Enums;

namespace ControleGastos.Modelos;

/// <summary>
/// Representa uma receita ou despesa vinculada a uma pessoa.
/// </summary>
public class TransacaoEntidade
{
    public int Id { get; set; }

    public required string Descricao { get; set; }

    /// <summary>
    /// Valor monetário armazenado em centavos.
    /// Exemplo: R$ 125,50 é armazenado como 12550.
    /// </summary>
    public long ValorEmCentavos { get; set; }

    public TipoTransacaoEnum Tipo { get; set; }

    /// <summary>Chave estrangeira da pessoa.</summary>
    public int PessoaId { get; set; }

    /// <summary>Propriedade de navegação para a pessoa.</summary>
    public PessoaEntidade Pessoa { get; set; } = null!;
}
