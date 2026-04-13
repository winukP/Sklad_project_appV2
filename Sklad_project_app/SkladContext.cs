using Microsoft.EntityFrameworkCore;
using Sklad_project_app.Models;


namespace Sklad_project_app
{
    public class SkladContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentItem> ShipmentItems { get; set; }

        public SkladContext() { }

        public SkladContext(DbContextOptions<SkladContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(DbConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //roles
            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<Role>().HasKey(role => role.Id);
            modelBuilder.Entity<Role>().Property(role => role.Id).HasColumnName("id");
            modelBuilder.Entity<Role>().Property(role => role.RoleName).HasColumnName("role");

            //users
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(user => user.Name).HasColumnName("name");
            modelBuilder.Entity<User>().Property(user => user.Surname).HasColumnName("surname");
            modelBuilder.Entity<User>().Property(user => user.Patronymic).HasColumnName("patronymic");
            modelBuilder.Entity<User>().Property(user => user.Role_id).HasColumnName("role_id");
            modelBuilder.Entity<User>().Property(user => user.Login).HasColumnName("login");
            modelBuilder.Entity<User>().Property(user => user.Password).HasColumnName("password");
            modelBuilder.Entity<User>()
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.Role_id);

            //categories
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Category>().HasKey(category => category.Id);
            modelBuilder.Entity<Category>().Property(category => category.Id).HasColumnName("id");
            modelBuilder.Entity<Category>().Property(category => category.Name).HasColumnName("name");

            //units
            modelBuilder.Entity<Unit>().ToTable("units");
            modelBuilder.Entity<Unit>().HasKey(unit => unit.Id);
            modelBuilder.Entity<Unit>().Property(unit => unit.Id).HasColumnName("id");
            modelBuilder.Entity<Unit>().Property(unit => unit.Name).HasColumnName("name");

            //Products
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(product => product.Id);
            modelBuilder.Entity<Product>().Property(product => product.Id).HasColumnName("id");
            modelBuilder.Entity<Product>().Property(product => product.Article).HasColumnName("article");
            modelBuilder.Entity<Product>().Property(product => product.Name).HasColumnName("name");
            modelBuilder.Entity<Product>().Property(product => product.CategoryId).HasColumnName("category_id");
            modelBuilder.Entity<Product>().Property(product => product.UnitId).HasColumnName("unit_id");
            modelBuilder.Entity<Product>()
                .HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);
            modelBuilder.Entity<Product>()
                .HasOne(product => product.Unit)
                .WithMany(unit => unit.Products)
                .HasForeignKey(product => product.UnitId);

            //stock
            modelBuilder.Entity<Stock>().ToTable("stock");
            modelBuilder.Entity<Stock>().HasKey(stock => stock.Id);
            modelBuilder.Entity<Stock>().Property(stock => stock.Id).HasColumnName("id");
            modelBuilder.Entity<Stock>().Property(stock => stock.ProductId).HasColumnName("product_id");
            modelBuilder.Entity<Stock>().Property(stock => stock.PurchasePrice).HasColumnName("purchase_price");
            modelBuilder.Entity<Stock>().Property(stock => stock.Rest).HasColumnName("rest");
            modelBuilder.Entity<Stock>()
                .HasOne(stock => stock.Product)
                .WithOne(product => product.Stock)
                .HasForeignKey<Stock>(stock => stock.ProductId);

            //clients
            modelBuilder.Entity<Client>().ToTable("clients");
            modelBuilder.Entity<Client>().HasKey(client => client.Id);
            modelBuilder.Entity<Client>().Property(client => client.Id).HasColumnName("id");
            modelBuilder.Entity<Client>().Property(client => client.Name).HasColumnName("name");

            //shipments
            modelBuilder.Entity<Shipment>().ToTable("shipments");
            modelBuilder.Entity<Shipment>().HasKey(shipment => shipment.Id);
            modelBuilder.Entity<Shipment>().Property(shipment => shipment.Id).HasColumnName("id");
            modelBuilder.Entity<Shipment>().Property(shipment => shipment.ClientId).HasColumnName("client_id");
            modelBuilder.Entity<Shipment>().Property(shipment => shipment.UserId).HasColumnName("user_id");
            modelBuilder.Entity<Shipment>().Property(shipment => shipment.ShipmentDate).HasColumnName("shipment_date");
            modelBuilder.Entity<Shipment>()
                .HasOne(shipment => shipment.Client)
                .WithMany(client => client.Shipments)
                .HasForeignKey(shipment => shipment.ClientId);

            //shipment_items
            modelBuilder.Entity<ShipmentItem>().ToTable("shipment_items");
            modelBuilder.Entity<ShipmentItem>().HasKey(item => item.Id);
            modelBuilder.Entity<ShipmentItem>().Property(item => item.Id).HasColumnName("id");
            modelBuilder.Entity<ShipmentItem>().Property(item => item.ShipmentId).HasColumnName("shipment_id");
            modelBuilder.Entity<ShipmentItem>().Property(item => item.ProductId).HasColumnName("product_id");
            modelBuilder.Entity<ShipmentItem>().Property(item => item.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<ShipmentItem>()
                .HasOne(item => item.Shipment)
                .WithMany(shipment => shipment.ShipmentItems)
                .HasForeignKey(item => item.ShipmentId);
            modelBuilder.Entity<ShipmentItem>()
                .HasOne(item => item.Product)
                .WithMany()
                .HasForeignKey(item => item.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}