using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assessment7_NET.Models;

public partial class SamuelchamizodatabaseContext : DbContext
{
    public SamuelchamizodatabaseContext()
    {
    }

    public SamuelchamizodatabaseContext(DbContextOptions<SamuelchamizodatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-OU08GV8\\SQLEXPRESS;Database=samuelchamizodatabase;Trusted_Connection=True;Encrypt=False;");
    */

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {



        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.IdClaim).HasName("PK__claims__23523C6741BE6256");

            entity.ToTable("claims");

            entity.Property(e => e.IdClaim)
                .ValueGeneratedNever()
                .HasColumnName("id_claim");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdVehicle).HasColumnName("id_vehicle");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.IdVehicleNavigation).WithMany(p => p.Claims)
                .HasForeignKey(d => d.IdVehicle)
                .HasConstraintName("FK__claims__id_vehic__29572725");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.IdOwner).HasName("PK__owners__390DB56E8E1AF5A2");

            entity.ToTable("owners");

            entity.Property(e => e.IdOwner)
                .ValueGeneratedNever()
                .HasColumnName("id_owner");
            entity.Property(e => e.DriverLicense)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("driverLicense");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("lastName");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.IdVehicle).HasName("PK__vehicles__6DF73CE43C18003C");

            entity.ToTable("vehicles");

            entity.Property(e => e.IdVehicle)
                .ValueGeneratedNever()
                .HasColumnName("id_vehicle");
            entity.Property(e => e.Brand)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.IdOwner).HasColumnName("id_owner");
            entity.Property(e => e.Vin)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("vin");
            entity.Property(e => e.Year)
                .HasColumnType("date")
                .HasColumnName("year");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK__vehicles__id_own__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
