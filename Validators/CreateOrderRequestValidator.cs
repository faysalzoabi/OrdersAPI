using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using OrderApi.Models;
using OrderApi.Models.Request;

namespace OrderApi.Validators
{
  public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
  {
    public CreateOrderRequestValidator()
    {
      RuleFor(x => x.Items)
              .NotNull()
              .Must(items => items is not null && items.Count > 0)
              .WithMessage("Items must contain at least one element");
    }
  }
}