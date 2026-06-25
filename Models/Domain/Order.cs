namespace OrderApi.Models.Domain
{
  public class Order
  {
    public int Id { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal TotalPrice { get; set; }
  }
}