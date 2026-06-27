using Microsoft.EntityFrameworkCore;
using OrderApi.Models.Domain;

namespace OrderApi.Data
{
  public class AppDbContext : DbContext
  {
    public DbSet<Order> orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
  }
}