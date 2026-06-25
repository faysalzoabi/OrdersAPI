using OrderApi.Models;
using OrderApi.Models.Domain;
using OrderApi.Models.Request;

namespace OrderApi.Services.Contracts
{
  public interface IOrderService
  {
    public List<Order> GetOrders();
    public Order? GetOrder(int id);
    public Order CreateOrder(CreateOrderRequest request);
  }

}