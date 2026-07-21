namespace ControleGastos.Diversos;

/// <summary> 
/// Centraliza as conversões monetárias entre decimal e centavos. 
/// </summary> 
public static class DinheiroHelper
{
    public static long ParaCentavos(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(valor),
                "O valor deve ser maior que zero.");
        }

        var arredondado = decimal.Round(
            valor,
            2,
            MidpointRounding.AwayFromZero);

        return checked((long)(arredondado * 100m));
    }

    public static decimal ParaDecimal(long centavos)
    {
        return centavos / 100m;
    }
} 