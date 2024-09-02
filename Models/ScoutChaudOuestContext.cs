using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScoutCO_API.Models;

public partial class ScoutChaudOuestContext : DbContext
{
    public ScoutChaudOuestContext()
    {
    }

    public ScoutChaudOuestContext(DbContextOptions<ScoutChaudOuestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<EquipmentCondition> EquipmentConditions { get; set; }

    public virtual DbSet<EquipmentRequest> EquipmentRequests { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Sponsor> Sponsors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlServer("Name=ScoutsConnectionString");
		optionsBuilder.EnableSensitiveDataLogging();
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Child>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(75);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(75);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.FkParent1).HasColumnName("FK_Parent_1");
            entity.Property(e => e.FkParent2).HasColumnName("FK_Parent_2");
            entity.Property(e => e.Gender).HasMaxLength(25);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(6);
            entity.Property(e => e.Province).HasMaxLength(50);

            entity.HasOne(d => d.FkParent1Navigation).WithMany(p => p.ChildFkParent1Navigations)
                .HasForeignKey(d => d.FkParent1)
                .HasConstraintName("FK_Children_Parent_1");
            
            entity.HasOne(d => d.FkParent2Navigation).WithMany(p => p.ChildFkParent2Navigations)
                .HasForeignKey(d => d.FkParent2)
                .HasConstraintName("FK_Children_Parent_2");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FkCondtion).HasColumnName("FK_Condtion");
            entity.Property(e => e.FkEmployeeLastUsed).HasColumnName("FK_EmployeeLastUsed");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.FkCondtionNavigation).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.FkCondtion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipment_Equipment_Condition");
        });

        modelBuilder.Entity<EquipmentCondition>(entity =>
        {
            entity.ToTable("Equipment_Condition");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Condition).HasMaxLength(50);
        });

        modelBuilder.Entity<EquipmentRequest>(entity =>
        {
            entity.ToTable("Equipment_Request");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FkEmployee).HasColumnName("FK_Employee");
            entity.Property(e => e.FkEquipment).HasColumnName("FK_Equipment");

            entity.HasOne(d => d.FkEmployeeNavigation).WithMany(p => p.EquipmentRequests)
                .HasForeignKey(d => d.FkEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipment_Request_Employee");

            entity.HasOne(d => d.FkEquipmentNavigation).WithMany(p => p.EquipmentRequests)
                .HasForeignKey(d => d.FkEquipment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipment_Request_Equipment");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AlbumlLink).HasMaxLength(200);
            entity.Property(e => e.ThumbnailLink).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.ToTable("Parent");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
				.HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(25);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Text).HasMaxLength(1000);
            entity.Property(e => e.ThumbnailLink).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Sponsor>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Text).HasMaxLength(200);
            entity.Property(e => e.ThumbnailLink).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(75);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
