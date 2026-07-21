using ControleGastos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Data;

/// <summary> 
/// Contexto principal de acesso ao banco SQLite. 
/// Representa uma sessão de consulta e gravação do EF Core. 
/// </summary> 
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<PessoaEntidade> Pessoas => Set<PessoaEntidade>();
    public DbSet<TransacaoEntidade> Transacoes => Set<TransacaoEntidade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurarPessoa(modelBuilder);
        ConfigurarTransacao(modelBuilder);
    }

    private static void ConfigurarPessoa(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PessoaEntidade>(entity =>
        {
            entity.ToTable(
                "Pessoas",
                tabela => tabela.HasCheckConstraint(
                    "CK_Pessoas_Idade",
                    "Idade >= 0 AND Idade <= 150"));

            entity.HasKey(pessoa => pessoa.Id);

            entity.Property(pessoa => pessoa.Id)
                .ValueGeneratedOnAdd();

            entity.Property(pessoa => pessoa.Nome)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(pessoa => pessoa.Idade)
                .IsRequired();
        });
    }

    private static void ConfigurarTransacao(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransacaoEntidade>(entity =>
        {
            entity.ToTable(
                "Transacoes",
                tabela =>
                {
                    tabela.HasCheckConstraint(
                        "CK_Transacoes_Valor",
                        "ValorEmCentavos > 0");

                    tabela.HasCheckConstraint(
                        "CK_Transacoes_Tipo",
                        "Tipo IN (1, 2)");
                });

            entity.HasKey(transacao => transacao.Id);

            entity.Property(transacao => transacao.Id)
                .ValueGeneratedOnAdd();

            entity.Property(transacao => transacao.Descricao)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(transacao => transacao.ValorEmCentavos)
                .IsRequired();

            entity.Property(transacao => transacao.Tipo)
                .IsRequired();

            entity.Property(transacao => transacao.PessoaId)
                .IsRequired();

            entity.HasIndex(transacao => transacao.PessoaId);

 
            entity.HasOne(transacao => transacao.Pessoa)
                .WithMany(pessoa => pessoa.TransacaoEntidade)
                .HasForeignKey(transacao => transacao.PessoaId)

                // Ao excluir uma pessoa, suas transações também 
                // são apagadas pelo próprio relacionamento. 
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}