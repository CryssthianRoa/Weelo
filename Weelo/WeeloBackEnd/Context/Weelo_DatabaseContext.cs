using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WeeloBackEnd.Models;

namespace WeeloBackEnd.Context
{
    public partial class Weelo_DatabaseContext : DbContext
    {
        private string _connectionString;
        public Weelo_DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Weelo_DatabaseContext(DbContextOptions<Weelo_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Property> Properties { get; set; } = null!;
        public virtual DbSet<PropertyImage> PropertyImages { get; set; } = null!;
        public virtual DbSet<PropertyTrace> PropertyTraces { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                //optionsBuilder.UseSqlServer("Server=DESKTOP-S4Q4131\\SQLEXPRESS; DataBase=Weelo_Database; User=sa; Password=dM57Me4KSb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyImage>().HasOne(p => p.IdPropertyNavigation).WithMany(b => b.PropertyImages)
                                .HasForeignKey(p => p.IdProperty)
                                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PropertyTrace>().HasOne(p => p.IdPropertyNavigation).WithMany(b => b.PropertyTraces)
                                .HasForeignKey(p => p.IdProperty)
                                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasKey(e => e.IdAudit)
                    .HasName("PK__Audit__C87E13DD856BB275");

                entity.ToTable("Audit", "Weelo");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Error)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Audits)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Audit__IdRole__534D60F1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Audits)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Audit__IdUser__52593CB8");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.IdOwner)
                    .HasName("PK__Owner__D326181692687B7A");

                entity.ToTable("Owner", "Weelo");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.IdProperty)
                    .HasName("PK__Property__842B6AA76DA8D5BC");

                entity.ToTable("Property", "Weelo");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CodeInternal)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(19, 8)");

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.IdOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Property__IdOwne__38996AB5");
            });

            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.HasKey(e => e.IdPropertyImage)
                    .HasName("PK__Property__018BACD5A53E9335");

                entity.ToTable("PropertyImage", "Weelo");

                entity.Property(e => e.File)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyImages)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PropertyI__IdPro__3B75D760");
            });

            modelBuilder.Entity<PropertyTrace>(entity =>
            {
                entity.HasKey(e => e.IdPropertyTrace)
                    .HasName("PK__Property__373407C92C22614E");

                entity.ToTable("PropertyTrace", "Weelo");

                entity.Property(e => e.DateSale).HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tax).HasColumnType("numeric(10, 4)");

                entity.Property(e => e.Value).HasColumnType("numeric(19, 8)");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyTraces)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PropertyT__IdPro__3E52440B");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__Role__B43690543FC057D7");

                entity.ToTable("Role", "Weelo");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C926388AEDFDBA");

                entity.ToTable("User", "Weelo");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.User1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("User");

                entity.Property(e => e.Value)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.IdUserRole)
                    .HasName("PK__UserRole__54B6C1BDD225669B");

                entity.ToTable("UserRole", "Weelo");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__IdRole__4E88ABD4");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__IdUser__4D94879B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
