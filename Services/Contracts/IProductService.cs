using OrderApi.Models.Domain;

namespace OrderApi.Services.Contracts
{
  public interface IProductService
  {
    List<Product> GetProducts(GetProductsQuery query);
    Product? GetProduct(int id);
  }
}