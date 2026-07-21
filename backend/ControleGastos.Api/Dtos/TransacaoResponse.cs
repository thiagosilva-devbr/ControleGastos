using ControleGastos.Enums;

namespace ControleGastos.Dtos;

public record TransacaoResponse(
    int Id,
    string Descricao,
    decimal Valor,
    TipoTransacaoEnum Tipo,
    int PessoaId,
    string NomePessoa);