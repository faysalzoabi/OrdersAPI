using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using OrderApi.Data;
using OrderApi.Models;
using OrderApi.Models.Domain;
using OrderApi.Models.Request;
using OrderApi.Services.Contracts;

namespace OrderApi.Services
{
  public class OrderService : IOrderService
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrderService(IMapper mapper, AppDbContext context)
    {
      _mapper = mapper;
      _context = context;
    }

    public List<Order> GetOrders()
    {
      var orders = _context.orders.ToList();
      return orders;
    }

    public Order? GetOrder(int id)
    {
      if (id < 0)
      {
        throw new ArgumentException("Invalid order id");
      }
      var order = _context.orders.FirstOrDefault(item => item.Id == id);

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

        var product = _context.Products.FirstOrDefault(i => i.Id == item.ProductId);

        if (product is null)
        {
          throw new ArgumentException($"Invalid product id {item.ProductId}");
        }

        totalPrice += product.Price * item.Quantity;

        orderItems.Add(_mapper.Map<OrderItem>(item));
      }

      var order = new Order
      {
        Items = orderItems,
        TotalPrice = totalPrice
      };

      _context.orders.Add(order);
      _context.SaveChanges();

      return order;
    }
  }
}