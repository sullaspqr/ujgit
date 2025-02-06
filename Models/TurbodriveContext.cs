using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjektNeveBackend.Models;

public partial class TurbodriveContext : DbContext
{
    public TurbodriveContext()
    {
    }

    public TurbodriveContext(DbContextOptions<TurbodriveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=turbodrive;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("car");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Mileage)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.RentalPricePerDay)
                .HasPrecision(10)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Year)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PRIMARY");

            entity.ToTable("contracts");

            entity.HasIndex(e => e.RentalId, "rental_id");

            entity.Property(e => e.ContractId)
                .HasColumnType("int(11)")
                .HasColumnName("contract_id");
            entity.Property(e => e.ContractDetails)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("contract_details");
            entity.Property(e => e.RentalId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("rental_id");
            entity.Property(e => e.SignedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("signed_date");

            entity.HasOne(d => d.Rental).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("contracts_ibfk_1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("address");
            entity.Property(e => e.DriverLicense)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("driver_license");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("phone_number");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("registration_date");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("customers_ibfk_1");
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("PRIMARY");

            entity.ToTable("maintenance");

            entity.HasIndex(e => e.CarId, "car_id");

            entity.Property(e => e.MaintenanceId)
                .HasColumnType("int(11)")
                .HasColumnName("maintenance_id");
            entity.Property(e => e.CarId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("car_id");
            entity.Property(e => e.Cost)
                .HasPrecision(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("cost");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.MaintenanceDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("maintenance_date");

            entity.HasOne(d => d.Car).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("maintenance_ibfk_1");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PRIMARY");

            entity.ToTable("payments");

            entity.HasIndex(e => e.RentalId, "rental_id");

            entity.Property(e => e.PaymentId)
                .HasColumnType("int(11)")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("payment_method");
            entity.Property(e => e.RentalId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("rental_id");

            entity.HasOne(d => d.Rental).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("payments_ibfk_1");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PRIMARY");

            entity.ToTable("rentals");

            entity.HasIndex(e => e.CarId, "car_id");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.Property(e => e.RentalId)
                .HasColumnType("int(11)")
                .HasColumnName("rental_id");
            entity.Property(e => e.CarId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("car_id");
            entity.Property(e => e.CustomerId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.RentalDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("rental_date");
            entity.Property(e => e.ReturnDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("return_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("total_price");

            entity.HasOne(d => d.Car).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rentals_ibfk_2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rentals_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.FelhasznaloNev, "FelhasznaloNev").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Aktiv).HasColumnType("int(1)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FelhasznaloNev).HasMaxLength(100);
            entity.Property(e => e.FenykepUtvonal).HasMaxLength(64);
            entity.Property(e => e.Hash)
                .HasMaxLength(64)
                .HasColumnName("HASH");
            entity.Property(e => e.Jogosultsag).HasColumnType("int(1)");
            entity.Property(e => e.RegisztracioDatuma)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime");
            entity.Property(e => e.Salt)
                .HasMaxLength(64)
                .HasColumnName("SALT");
            entity.Property(e => e.TeljesNev).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
