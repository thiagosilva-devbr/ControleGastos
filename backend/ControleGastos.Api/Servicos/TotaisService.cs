using ControleGastos.Data;
using ControleGastos.Dtos;
using ControleGastos.Enums;
using ControleGastos.Diversos;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Services;

/// <summary>Calcula totais individuais e gerais.</summary> 
public class TotaisService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<ConsultaTotaisResponse> ConsultarAsync(
        CancellationToken cancellationToken)
    {
        // A consulta começa em Pessoas para também retornar 
        // pessoas que ainda não possuem transações. 
        var dados = await _context.Pessoas
            .AsNoTracking()
            .OrderBy(pessoa => pessoa.Nome)
            .Select(pessoa => new
            {
                pessoa.Id,
                pessoa.Nome,

                Receitas = pessoa.TransacaoEntidade
                    .Where(transacaoEntidade =>
                        transacaoEntidade.Tipo == TipoTransacaoEnum.Receita)
                    .Sum(transacao =>
                        (long?)transacao.ValorEmCentavos) ?? 0,

                Despesas = pessoa.TransacaoEntidade
                    .Where(transacao =>
                        transacao.Tipo == TipoTransacaoEnum.Despesa)
                    .Sum(transacao =>
                        (long?)transacao.ValorEmCentavos) ?? 0
            })
            .ToListAsync(cancellationToken);

        var pessoas = dados
            .Select(item =>
            {
                var saldo = item.Receitas - item.Despesas;

                return new TotalPessoaResponse(
                    item.Id,
                    item.Nome,
                    DinheiroHelper.ParaDecimal(item.Receitas),
                    DinheiroHelper.ParaDecimal(item.Despesas),
                    DinheiroHelper.ParaDecimal(saldo));
            })
            .ToList();

        var totalReceitas = pessoas.Sum(
            pessoa => pessoa.TotalReceitas);

        var totalDespesas = pessoas.Sum(
            pessoa => pessoa.TotalDespesas);

        var totalGeral = new TotalGeralResponse(
            totalReceitas,
            totalDespesas,
            totalReceitas - totalDespesas);

        return new ConsultaTotaisResponse(
            pessoas,
            totalGeral);
    }
}