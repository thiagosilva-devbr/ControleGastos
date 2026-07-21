namespace ControleGastos.Dtos;

public record ConsultaTotaisResponse(
    IReadOnlyCollection<TotalPessoaResponse> Pessoas,
    TotalGeralResponse TotalGeral); 