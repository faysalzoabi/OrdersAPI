
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;
using OrderApi.Models.Request;
using OrderApi.Models.Response;
using OrderApi.Services;
using OrderApi.Services.Contracts;

namespace OrderApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
      _orderService = orderService;
      _mapper = mapper;
    }
    [HttpGet]
    public IActionResult GetOrders()
    {
      var orders = _orderService.GetOrders();

      var response = _mapper.Map<List<OrderResponse>>(orders);

      var finalResponse = new ApiResponse<IEnumerable<OrderResponse>>
      {
        Data = response,
        Message = "Orders Retrieved Successfully"
      };

      return Ok(finalResponse);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrder([FromRoute] int id)
    {
      try
      {
        var order = _orderService.GetOrder(id);

        var response = _mapper.Map<OrderResponse>(order);

        var finalResponse = new ApiResponse<OrderResponse>
        {
          Data = response,
          Message = "Order retrieved successfully"
        };

        if (order is null)
        {
          return NotFound();
        }

        return Ok(finalResponse);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      }

    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
      try
      {
        var order = _orderService.CreateOrder(request);

        var response = _mapper.Map<OrderResponse>(order);

        var finalResponse = new ApiResponse<OrderResponse>
        {
          Data = response,
          Message = "Order created Successfully"
        };

        return Ok(finalResponse);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      }

    }
  }
}