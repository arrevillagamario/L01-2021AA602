﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace L01_2021AA602.Models;

public partial class RestauranteContext : DbContext
{
    public RestauranteContext()
    {
    }

    public RestauranteContext(DbContextOptions<RestauranteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Motorista> Motoristas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Plato> Platos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FEGH0CR\\SQLEXPRESS; Database=restaurante; Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__clientes__C2FF245D8DE79CB3");

            entity.ToTable("clientes");

            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombreCliente");
        });

        modelBuilder.Entity<Motorista>(entity =>
        {
            entity.HasKey(e => e.MotoristaId).HasName("PK__motorist__FBA844A4A8AAAA67");

            entity.ToTable("motoristas");

            entity.Property(e => e.MotoristaId).HasColumnName("motoristaId");
            entity.Property(e => e.NombreMotorista)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombreMotorista");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("PK__pedidos__BAF07B044BFB2519");

            entity.ToTable("pedidos");

            entity.Property(e => e.PedidoId).HasColumnName("pedidoId");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.MotoristaId).HasColumnName("motoristaId");
            entity.Property(e => e.PlatoId).HasColumnName("platoId");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(18, 4)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<Plato>(entity =>
        {
            entity.HasKey(e => e.PlatoId).HasName("PK__platos__F55C90B09C197859");

            entity.ToTable("platos");

            entity.Property(e => e.PlatoId).HasColumnName("platoId");
            entity.Property(e => e.NombrePlato)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombrePlato");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(18, 4)")
                .HasColumnName("precio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
