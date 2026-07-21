namespace ControleGastos.Dtos;

public record TotalPessoaResponse(
    int PessoaId,
    string Nome,
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo); 