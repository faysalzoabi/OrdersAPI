using System.ComponentModel.DataAnnotations;
using OrderApi.Models.Request;

namespace OrderApi.Models.Request
{
  public class CreateOrderRequest
  {
    [Required]
    [MinLength(1)]
    public List<OrderItemDto> Items { get; set; }
  }
}