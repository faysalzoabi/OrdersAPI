using OrderApi.Models.Domain;

namespace OrderApi.Services.Contracts
{
  public interface IProductService
  {
    List<Product> GetProducts();
    Product? GetProduct(int id);
  }
}