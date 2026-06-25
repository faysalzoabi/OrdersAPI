using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OrderApi.Filters
{
  public class ValidateModelFilter : IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (context.ModelState.IsValid == false)
      {
        context.Result = new BadRequestObjectResult(context.ModelState);
      }
    }
  }
}