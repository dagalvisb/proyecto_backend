using Microsoft.EntityFrameworkCore;

namespace Usuarios.server.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Materia> Materias { get; set; } = null!;
        public DbSet<incMaterias> incMaterias { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.dni).IsUnique();
            modelBuilder.Entity<Materia>().HasIndex(m => m.codigo).IsUnique();
            modelBuilder.Entity<incMaterias>().HasIndex(i => i.usuario);

        }
    }
}
