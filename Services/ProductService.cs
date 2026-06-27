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

    public List<Product> GetProducts(GetProductsQuery query)
    {
      IQueryable<Product> productQuery = _context.Products;

      if (query.MinPrice.HasValue)
      {
        productQuery = productQuery.Where(product => product.Price >= query.MinPrice);
      }

      if (!string.IsNullOrWhiteSpace(query.SortBy))
      {
        switch (query.SortBy?.ToLowerInvariant())
        {
          case "price_asc":
            productQuery = productQuery.OrderBy(product => product.Price);
            break;
          case "price_desc":
            productQuery = productQuery.OrderByDescending(product => product.Price);
            break;
        }
      }

      return productQuery
                  .Skip((query.Page - 1) * query.PageSize)
                  .Take(query.PageSize)
                  .ToList();
    }
  }
}