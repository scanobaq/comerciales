using comerciales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace comercial.Infrastructure.Persistence.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comerciante> Comerciantes { get; set; }

    public virtual DbSet<Establecimiento> Establecimientos { get; set; }

    public virtual DbSet<EstadoComerciante> EstadoComerciantes { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comerciante>(entity =>
         {
             entity.ToTable("Comerciante", "reg", tb => tb.HasTrigger("TR_Comerciante_Audit"));

             entity.HasIndex(e => e.Correo, "IX_Comerciante_Correo")
                 .IsUnique()
                 .HasFilter("([Correo] IS NOT NULL)");

             entity.Property(e => e.EstadoId).HasDefaultValue(1);
             entity.Property(e => e.FechaActualizacionUtc).HasDefaultValueSql("(sysutcdatetime())");
             entity.Property(e => e.FechaRegistroUtc).HasDefaultValueSql("(sysutcdatetime())");
             entity.Property(e => e.UsuarioActualizacionId).HasDefaultValue(1);

             entity.HasOne(d => d.Estado).WithMany(p => p.Comerciantes)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Comerciante_EstadoComerciante");

             entity.HasOne(d => d.Municipio).WithMany(p => p.Comerciantes)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Comerciante_Municipio");

             entity.HasOne(d => d.UsuarioActualizacion).WithMany(p => p.Comerciantes)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Comerciante_UsuarioActualizacion");
         });

        modelBuilder.Entity<Establecimiento>(entity =>
       {
           entity.ToTable("Establecimiento", "reg", tb => tb.HasTrigger("TR_Establecimiento_Audit"));

           entity.Property(e => e.FechaActualizacionUtc).HasDefaultValueSql("(sysutcdatetime())");
           entity.Property(e => e.UsuarioActualizacionId).HasDefaultValue(1);

           entity.HasOne(d => d.Comerciante).WithMany(p => p.Establecimientos)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Establecimiento_Comerciante");

           entity.HasOne(d => d.UsuarioActualizacion).WithMany(p => p.Establecimientos)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Establecimiento_UsuarioActualizacion");
       });

        modelBuilder.Entity<Usuario>(entity =>
         {
             entity.Property(e => e.Activo).HasDefaultValue(true);
             entity.Property(e => e.CreadoUtc).HasDefaultValueSql("(sysutcdatetime())");
         });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
