
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Models.Response;
using OrderApi.Services.Contracts;

namespace OrderApi.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ProductsController : ControllerBase
  {
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
      _productService = productService;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetProducts([FromQuery] GetProductsQuery query)
    {
      var products = _productService.GetProducts(query);
      var response = _mapper.Map<List<ProductResponse>>(products);

      var finalResponse = new ApiResponse<List<ProductResponse>>
      {
        Data = response,
        Message = "Products retrieved Successfully"
      };

      return Ok(finalResponse);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
      var product = _productService.GetProduct(id);
      if (product is null)
      {
        return NotFound(new ApiResponse<ProductResponse>
        {
          Data = null,
          Message = $"Product with id {id} was not found."
        });
      }
      var response = _mapper.Map<ProductResponse>(product);

      var finalResponse = new ApiResponse<ProductResponse>
      {
        Data = response,
        Message = "Products retrieved Successfully"
      };

      return Ok(finalResponse);
    }
  }
}