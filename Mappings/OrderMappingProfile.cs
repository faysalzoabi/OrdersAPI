using AutoMapper;
using OrderApi.Models.Domain;
using OrderApi.Models.Request;
using OrderApi.Models.Response;

namespace OrderApi.Mappings
{
  public class OrderMappingProfile : Profile
  {
    public OrderMappingProfile()
    {
      CreateMap<OrderItemDto, OrderItem>();
      CreateMap<Order, OrderResponse>();
    }

  }
}