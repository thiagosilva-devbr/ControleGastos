using System.ComponentModel.DataAnnotations;
using ControleGastos.Enums;

namespace ControleGastos.Dtos;

/// <summary>Dados recebidos para cadastrar uma transação.</summary> 
public class CriarTransacaoRequest
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(250, MinimumLength = 2,
        ErrorMessage = "A descrição deve possuir entre 2 e 250 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    [EnumDataType(typeof(TipoTransacaoEnum),
        ErrorMessage = "O tipo deve ser Despesa ou Receita.")]
    public TipoTransacaoEnum Tipo { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "Informe uma pessoa válida.")]
    public int PessoaId { get; set; }
}

