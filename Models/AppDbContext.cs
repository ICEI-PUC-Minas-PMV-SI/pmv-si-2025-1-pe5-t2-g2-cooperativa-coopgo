using Microsoft.EntityFrameworkCore;
using CadastroUsuarios.Models;
using COOPGO.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.nome)
            .IsUnique()
            .HasDatabaseName("IX_Usuarios_Nome_Unique");


        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Usuario)
            .WithMany(u => u.Transacoes)
            .HasForeignKey(t => t.usuarioId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Transacao>()
            .Property(t => t.valor)
            .HasPrecision(10, 2);


        modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
        modelBuilder.Entity<Transacao>().ToTable("Transacoes");
    }
}