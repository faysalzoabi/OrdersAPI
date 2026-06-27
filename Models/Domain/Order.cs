using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models.Domain
{
  [Table("Orders")]
  public class Order
  {
    [Key]
    public int Id { get; set; }
    public List<OrderItem> Items { get; set; } = new();

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; set; }

    [Column(TypeName = "navchar(50)")]
    public string Status { get; set; } = "Created";
  }
}