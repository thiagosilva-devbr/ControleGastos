using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Dtos;

/// <summary>Dados recebidos para cadastrar uma pessoa.</summary> 
public class CriarPessoaRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(150, MinimumLength = 2,
        ErrorMessage = "O nome deve possuir entre 2 e 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0, 120,
        ErrorMessage = "A idade deve estar entre 0 e 120 anos.")]
    public int Idade { get; set; }
} 