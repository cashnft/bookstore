using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.Entities;

namespace OnlineBookstore.Infrastructure.Data;

public class BookstoreContext : DbContext
{
    public BookstoreContext(DbContextOptions<BookstoreContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Inventory> Inventory { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;      // Add this
    public DbSet<OrderItem> OrderItems { get; set; } = null!;  // And this

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Existing configurations
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j => j.ToTable("BookAuthors"));

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Inventory)
            .WithOne(i => i.Book)
            .HasForeignKey<Inventory>(i => i.BookId);

        modelBuilder.Entity<Book>()
            .Property(b => b.Price)
            .HasPrecision(10, 2);

        // Add Order configurations
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.BookId });

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.PriceAtTime)
            .HasPrecision(10, 2);
    }
}