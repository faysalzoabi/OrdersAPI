using OrderApi.Data;
using OrderApi.Models.Domain;
using OrderApi.Services.Contracts;

namespace OrderApi.Services
{
  public class ProductService : IProductService
  {
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
      _context = context;
    }
    public Product? GetProduct(int id)
    {
      if (id < 0)
      {
        throw new ArgumentException("Invalid product id");
      }

      var product = _context.Products.FirstOrDefault(product => product.Id == id);

      return product;
    }

    public List<Product> GetProducts()
    {
      return _context.Products.ToList();
    }
  }
}