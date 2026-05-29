using BikeShop.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure;

public class BikeShopContext : DbContext
{
    public DbSet<BikeModel> Bikes => Set<BikeModel>();

    public DbSet<CustomerModel> Customers => Set<CustomerModel>();

    public DbSet<CustomerProfileModel> CustomerProfiles => Set<CustomerProfileModel>();

    public DbSet<OrderModel> Orders => Set<OrderModel>();

    // конструктор без параметрів потрібен для простого створення контексту 
    public BikeShopContext()
    {
    }

    // конструктор з параметрами можна залишити для гнучкості
    public BikeShopContext(DbContextOptions<BikeShopContext> options)
        : base(options)
    {
    }

    // рядок підключення до PostgreSQL
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=bikeshop_db;Username=postgres;Password=postgres"
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // BikeModel
        modelBuilder.Entity<BikeModel>()
            .ToTable("bikes");

        modelBuilder.Entity<BikeModel>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<BikeModel>()
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<BikeModel>()
            .Property(x => x.Brand)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<BikeModel>()
            .Property(x => x.FrameSize)
            .IsRequired()
            .HasMaxLength(10);

        modelBuilder.Entity<BikeModel>()
            .Property(x => x.Price)
            .HasColumnType("numeric(18,2)");

        // CustomerModel
        modelBuilder.Entity<CustomerModel>()
            .ToTable("customers");

        modelBuilder.Entity<CustomerModel>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<CustomerModel>()
            .Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<CustomerModel>()
            .Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(30);

        modelBuilder.Entity<CustomerModel>()
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<CustomerModel>()
            .Property(x => x.DiscountPercent)
            .HasColumnType("numeric(5,2)");

        // CustomerProfileModel
        modelBuilder.Entity<CustomerProfileModel>()
            .ToTable("customer_profiles");

        modelBuilder.Entity<CustomerProfileModel>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<CustomerProfileModel>()
            .Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(200);

        // один-до-одного: CustomerModel - CustomerProfileModel
        modelBuilder.Entity<CustomerModel>()
            .HasOne(x => x.Profile)
            .WithOne(x => x.Customer)
            .HasForeignKey<CustomerProfileModel>(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderModel
        modelBuilder.Entity<OrderModel>()
            .ToTable("orders");

        modelBuilder.Entity<OrderModel>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<OrderModel>()
            .Property(x => x.UnitPrice)
            .HasColumnType("numeric(18,2)");

        // один-до-багатьох: CustomerModel - OrderModel
        modelBuilder.Entity<CustomerModel>()
            .HasMany(x => x.Orders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // один-до-багатьох: BikeModel - OrderModel
        modelBuilder.Entity<BikeModel>()
            .HasMany(x => x.Orders)
            .WithOne(x => x.Bike)
            .HasForeignKey(x => x.BikeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}