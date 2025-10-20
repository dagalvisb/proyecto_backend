using Microsoft.EntityFrameworkCore;
using Usuarios.server.Models;

namespace Usuarios.server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // === Tablas ===
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<incMaterias> IncMaterias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Configuración de Usuario ===
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.lugarNacimiento).HasMaxLength(50);
                entity.Property(e => e.dni).HasMaxLength(50);
                entity.Property(e => e.correo).HasMaxLength(50);
                entity.Property(e => e.direccion).HasMaxLength(50);
                entity.Property(e => e.cp).HasMaxLength(50);
                entity.Property(e => e.ciudad).HasMaxLength(50);
                entity.Property(e => e.movil).HasMaxLength(50);
                entity.Property(e => e.firma).HasMaxLength(50);
                entity.Property(e => e.tipo_usuario).HasMaxLength(50);
                entity.Property(e => e.bloque1).HasMaxLength(500);
                entity.Property(e => e.bloque2).HasMaxLength(500);

                entity.Property(e => e.createdDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.updatedDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // === Configuración de Materia ===
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.ToTable("materias");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.codigo)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.materia)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.semestre).IsRequired();

                entity.Property(e => e.createdDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.updatedDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // === Configuración de incMaterias ===
            modelBuilder.Entity<incMaterias>(entity =>
            {
                entity.ToTable("incmaterias");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.usuario)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.materia)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.createdDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}

