using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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

    public Task<List<Order>> GetOrders()
    {
      var orders = _context.orders.Include(o => o.Items).ToListAsync();
      return orders;
    }

    public Task<Order?> GetOrder(int id)
    {
      if (id < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");
      }
      var order = _context.orders.Include(o => o.Items).FirstOrDefaultAsync(item => item.Id == id);

      return order;
    }

    public async Task<Order> CreateOrder(CreateOrderRequest request)
    {
      if (request.Items.Count > 10)
      {
        throw new ArgumentException("Cannot order more than 10 items");
      }

      System.Console.WriteLine("creating order with {0} items", request.Items.Count);

      decimal totalPrice = 0;

      var orderItems = new List<OrderItem>();
      var productIds = request.Items.Select(i => i.ProductId).ToList();
      var products = await _context.Products
                          .Where(p => productIds.Contains(p.Id))
                          .ToListAsync();

      var productMap = products.ToDictionary(p => p.Id);

      foreach (var item in request.Items)
      {
        if (item.Quantity <= 0)
        {
          throw new ArgumentException("Quantity must be greater than 0");
        }

        if (!productMap.TryGetValue(item.ProductId, out var product))
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
      await _context.SaveChangesAsync();

      return order;
    }
  }
}