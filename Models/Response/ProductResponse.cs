namespace OrderApi.Models.Response
{
  public class ProductResponse
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
  }
}