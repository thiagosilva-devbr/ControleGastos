namespace ControleGastos.Dtos;

public record TotalGeralResponse(
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo); 