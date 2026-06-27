
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
  [Authorize(Roles = "Admin")]
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
    public async Task<IActionResult> GetOrders()
    {
      var orders = await _orderService.GetOrders();

      var response = _mapper.Map<List<OrderResponse>>(orders);

      var finalResponse = new ApiResponse<IEnumerable<OrderResponse>>
      {
        Data = response,
        Message = "Orders Retrieved Successfully"
      };

      return Ok(finalResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder([FromRoute] int id)
    {
      try
      {
        var order = await _orderService.GetOrder(id);

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
      catch (ArgumentOutOfRangeException ex)
      {
        return BadRequest(ex.Message);
      }

    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
      try
      {
        var order = await _orderService.CreateOrder(request);

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