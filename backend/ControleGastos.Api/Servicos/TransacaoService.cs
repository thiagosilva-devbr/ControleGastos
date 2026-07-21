using ControleGastos.Data;
using ControleGastos.Dtos;
using ControleGastos.Enums;
using ControleGastos.Diversos;
using ControleGastos.Modelos;
using ControleGastos.excecoes.excecoes;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Services;

/// <summary> 
/// Implementa as operações e regras relacionadas às transações. 
/// </summary> 
public class TransacaoService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<TransacaoResponse> CriarAsync(
        CriarTransacaoRequest request,
        CancellationToken cancellationToken)
    {
        var pessoa = await _context.Pessoas
            .AsNoTracking()
            .FirstOrDefaultAsync(
                item => item.Id == request.PessoaId,
                cancellationToken);

        if (pessoa is null)
        {
            throw new RecursoNaoEncontradoException(
                "A pessoa informada não existe.");
        }

        // A validação precisa existir no back-end, pois uma chamada 
        // direta à API poderia ignorar as restrições da tela React. 
        if (pessoa.Idade < 18 &&
            request.Tipo == TipoTransacaoEnum.Receita)
        {
            throw new RegraNegocioException(
                "Pessoas menores de 18 anos somente podem " +
                "possuir despesas.");
        }

        var descricao = request.Descricao.Trim();

        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException(
                "A descrição da transação é obrigatória.");
        }

        var transacao = new TransacaoEntidade
        {
            Descricao = descricao,
            ValorEmCentavos =
                DinheiroHelper.ParaCentavos(request.Valor),
            Tipo = request.Tipo,
            PessoaId = pessoa.Id
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync(cancellationToken);

        return new TransacaoResponse(
            transacao.Id,
            transacao.Descricao,
            DinheiroHelper.ParaDecimal(
                transacao.ValorEmCentavos),
            transacao.Tipo,
            pessoa.Id,
            pessoa.Nome);
    }

    public async Task<IReadOnlyCollection<TransacaoResponse>> ListarAsync(
        CancellationToken cancellationToken)
    {
        var transacoes = await _context.Transacoes
            .AsNoTracking()
            .OrderByDescending(transacao => transacao.Id)
            .Select(transacao => new
            {
                transacao.Id,
                transacao.Descricao,
                transacao.ValorEmCentavos,
                transacao.Tipo,
                transacao.PessoaId,
                NomePessoa = transacao.Pessoa.Nome
            })
            .ToListAsync(cancellationToken);

        return transacoes
            .Select(transacao => new TransacaoResponse(
                transacao.Id,
                transacao.Descricao,
                DinheiroHelper.ParaDecimal(
                    transacao.ValorEmCentavos),
                transacao.Tipo,
                transacao.PessoaId,
                transacao.NomePessoa))
            .ToList();
    }
}