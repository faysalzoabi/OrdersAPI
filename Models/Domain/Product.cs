using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models.Domain
{
  [Table("Products")]
  public class Product
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();
  }
}