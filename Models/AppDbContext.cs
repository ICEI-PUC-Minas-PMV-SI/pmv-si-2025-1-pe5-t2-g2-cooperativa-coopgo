using CadastroUsuarios.Models;
using Microsoft.EntityFrameworkCore;

namespace COOPGO.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Transacao> Transacao { get; set; }

    }

}
