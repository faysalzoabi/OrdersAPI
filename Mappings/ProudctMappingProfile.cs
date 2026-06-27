using AutoMapper;
using OrderApi.Models.Domain;
using OrderApi.Models.Response;

namespace OrderApi.Mappings
{
  public class ProductMappingProfile : Profile
  {
    public ProductMappingProfile()
    {
      CreateMap<Product, ProductResponse>();
    }

  }
}