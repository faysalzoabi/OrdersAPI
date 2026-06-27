using System.Data;
using FluentValidation;

namespace OrderApi.Validators
{

  public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
  {
    public GetProductsQueryValidator()
    {
      RuleFor(x => x.Page)
          .GreaterThan(0)
          .WithMessage("Page must be greater than 0");

      RuleFor(x => x.PageSize)
          .InclusiveBetween(0, 20)
          .WithMessage("PageSize must be between 1 and 20");

      RuleFor(x => x.MinPrice)
          .GreaterThanOrEqualTo(0)
          .When(x => x.MinPrice.HasValue)
          .WithMessage("MinPrice must be >= 0");

      RuleFor(x => x.SortBy)
          .Must(sortBy =>
            string.IsNullOrWhiteSpace(sortBy) ||
            sortBy is "price_asc" or "price_desc")
            .WithMessage("SortBy must be 'price_asc' or 'price_desc'.");
    }
  }
}