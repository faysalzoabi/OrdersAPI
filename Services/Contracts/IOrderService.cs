using OrderApi.Models;
using OrderApi.Models.Domain;
using OrderApi.Models.Request;

namespace OrderApi.Services.Contracts
{
  public interface IOrderService
  {
    public Task<List<Order>> GetOrders();
    public Task<Order?> GetOrder(int id);
    public Task<Order> CreateOrder(CreateOrderRequest request);
  }

}