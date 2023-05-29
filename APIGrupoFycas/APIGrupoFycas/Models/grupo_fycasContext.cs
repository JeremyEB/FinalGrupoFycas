using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIGrupoFycas.Models
{
    public partial class grupo_fycasContext : DbContext
    {
        public grupo_fycasContext()
        {
        }

        public grupo_fycasContext(DbContextOptions<grupo_fycasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Historialfactura> Historialfacturas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=grupo_fycas;uid=root;convert zero datetime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.20-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PRIMARY");

                entity.ToTable("clientes");

                entity.Property(e => e.IdCliente)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_CLIENTE");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("APELLIDO");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(13)
                    .HasColumnName("CEDULA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(12)
                    .HasColumnName("TELEFONO");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PRIMARY");

                entity.ToTable("facturas");

                entity.HasIndex(e => e.ClienteId, "CLIENTE_ID");

                entity.Property(e => e.IdFactura)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_FACTURA");

                entity.Property(e => e.Capital)
                    .HasPrecision(9, 2)
                    .HasColumnName("CAPITAL");

                entity.Property(e => e.ClienteId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CLIENTE_ID");

                entity.Property(e => e.Cuotas)
                    .HasPrecision(9, 2)
                    .HasColumnName("CUOTAS");

                entity.Property(e => e.CuotasMensuales)
                    .HasPrecision(9, 2)
                    .HasColumnName("CUOTAS_MENSUALES");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Interes)
                    .HasPrecision(9, 2)
                    .HasColumnName("INTERES");

                entity.Property(e => e.MontoSolicitado)
                    .HasPrecision(9, 2)
                    .HasColumnName("MONTO_SOLICITADO");

                entity.Property(e => e.PagoNuevo)
                    .HasPrecision(9, 2)
                    .HasColumnName("PAGO_NUEVO");

                entity.Property(e => e.PagoRealizado)
                    .HasPrecision(9, 2)
                    .HasColumnName("PAGO_REALIZADO");

                entity.Property(e => e.Tasa)
                    .HasPrecision(5, 2)
                    .HasColumnName("TASA");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("facturas_ibfk_1");
            });

            modelBuilder.Entity<Historialfactura>(entity =>
            {
                entity.HasKey(e => e.IdHistorialfactura)
                    .HasName("PRIMARY");

                entity.ToTable("historialfacturas");

                entity.Property(e => e.IdHistorialfactura)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_HISTORIALFACTURA");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("APELLIDO");

                entity.Property(e => e.Capital)
                    .HasPrecision(9, 2)
                    .HasColumnName("CAPITAL");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(13)
                    .HasColumnName("CEDULA");

                entity.Property(e => e.ClienteId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CLIENTE_ID");

                entity.Property(e => e.Cuotas)
                    .HasPrecision(9, 2)
                    .HasColumnName("CUOTAS");

                entity.Property(e => e.CuotasMensuales)
                    .HasPrecision(9, 2)
                    .HasColumnName("CUOTAS_MENSUALES");

                entity.Property(e => e.FacturaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("FACTURA_ID");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Interes)
                    .HasPrecision(9, 2)
                    .HasColumnName("INTERES");

                entity.Property(e => e.MontoSolicitado)
                    .HasPrecision(9, 2)
                    .HasColumnName("MONTO_SOLICITADO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.PagoNuevo)
                    .HasPrecision(9, 2)
                    .HasColumnName("PAGO_NUEVO");

                entity.Property(e => e.PagoRealizado)
                    .HasPrecision(9, 2)
                    .HasColumnName("PAGO_REALIZADO");

                entity.Property(e => e.Tasa)
                    .HasPrecision(5, 2)
                    .HasColumnName("TASA");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(12)
                    .HasColumnName("TELEFONO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
