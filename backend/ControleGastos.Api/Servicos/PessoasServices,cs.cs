using ControleGastos.Data;
using ControleGastos.Dtos;
using ControleGastos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Services;

/// <summary> 
/// Implementa as operações e regras relacionadas às pessoas. 
/// </summary> 
public class PessoaService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<PessoaResponse> CriarAsync(
        CriarPessoaRequest request,
        CancellationToken cancellationToken)
    {
        var nome = request.Nome.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException(
                "O nome da pessoa é obrigatório.");
        }

        var pessoa = new PessoaEntidade
        {
            Nome = nome,
            Idade = request.Idade
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync(cancellationToken);

        return new PessoaResponse(
            pessoa.Id,
            pessoa.Nome,
            pessoa.Idade);
    }

    public async Task<IReadOnlyCollection<PessoaResponse>> ListarAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Pessoas
            .AsNoTracking()
            .OrderBy(pessoa => pessoa.Nome)
            .ThenBy(pessoa => pessoa.Id)
            .Select(pessoa => new PessoaResponse(
                pessoa.Id,
                pessoa.Nome,
                pessoa.Idade))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExcluirAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var pessoa = await _context.Pessoas
            .FindAsync([id], cancellationToken);

        if (pessoa is null)
        {
            return false;
        }

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync(cancellationToken);

        // As transações são apagadas automaticamente pelo cascade. 
        return true;
    }
}