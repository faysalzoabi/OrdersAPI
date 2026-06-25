using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using OrderApi.Models;
using OrderApi.Models.Domain;
using OrderApi.Models.Request;
using OrderApi.Services.Contracts;

namespace OrderApi.Services
{
  public class OrderService : IOrderService
  {
    private static List<Order> _orders = new List<Order>();
    private static List<Product> _products = new List<Product>
    {
      new Product {Id= 1, Name="Laptop", Price= 500},
      new Product {Id= 2, Name="Mouse", Price= 100},
      new Product {Id= 3, Name="Keyboard", Price= 300},
    };

    private readonly IMapper _mapper;

    public OrderService(IMapper mapper)
    {
      _mapper = mapper;
    }

    public List<Order> GetOrders()
    {
      return _orders;
    }

    public Order? GetOrder(int id)
    {
      if (id < 0)
      {
        throw new ArgumentException("Invalid order id");
      }
      var order = _orders.FirstOrDefault(item => item.Id == id);

      return order;
    }

    public Order CreateOrder(CreateOrderRequest request)
    {
      if (request.Items.Count > 10)
      {
        throw new ArgumentException("Cannot order more than 10 items");
      }

      System.Console.WriteLine("creating order with {0} items", request.Items.Count);

      decimal totalPrice = 0;

      var orderItems = new List<OrderItem>();

      foreach (var item in request.Items)
      {
        if (item.Quantity <= 0)
        {
          throw new ArgumentException("Quantity must be greater than 0");
        }

        var product = _products.FirstOrDefault(i => i.Id == item.ProductId);

        if (product is null)
        {
          throw new ArgumentException($"Invalid product id {item.ProductId}");
        }

        totalPrice += product.Price * item.Quantity;

        orderItems.Add(_mapper.Map<OrderItem>(item));
      }

      var order = new Order
      {
        Id = _orders.Count + 1,
        Items = orderItems,
        TotalPrice = totalPrice
      };

      _orders.Add(order);

      return order;
    }
  }
}