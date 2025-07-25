using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JguevaraProgramacionNcapasFebreroContext : DbContext
{
    public JguevaraProgramacionNcapasFebreroContext()
    {
    }

    public JguevaraProgramacionNcapasFebreroContext(DbContextOptions<JguevaraProgramacionNcapasFebreroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MateriaGetAllView> MateriaGetAllViews { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Semestre> Semestres { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<UsuarioLoginDTO> UsuarioLoginDTO { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuarioLoginDTO>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<MateriaGetAllView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MateriaGetAllView");

            entity.Property(e => e.Costo).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Creditos).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Fecha)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IdMateria).ValueGeneratedOnAdd();
            entity.Property(e => e.NombreMateria)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.HasKey(e => e.IdMateria);

            entity.Property(e => e.Costo).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Creditos).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSemestreNavigation).WithMany(p => p.Materia)
                .HasForeignKey(d => d.IdSemestre)
                .HasConstraintName("FK__Materia__IdSemes__182C9B23");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C36FF1403");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Semestre>(entity =>
        {
            entity.HasKey(e => e.IdSemestre).HasName("PK__Semestre__BD1FD7F845D12C87");

            entity.ToTable("Semestre");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF971CD5336C");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105346D50BD6F").IsUnique();

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(400);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
