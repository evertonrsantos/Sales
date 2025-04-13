using FluentValidation;

namespace SalesApi.Application.Commands.Validators
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale number is required");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required");
            RuleFor(x => x.BranchId).NotEmpty().WithMessage("Branch ID is required");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one item is required")
                .ForEach(item => {
                    item.ChildRules(i => {
                        i.RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required");
                        i.RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(20)
                            .WithMessage("Quantity must be between 1 and 20");
                        i.RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than zero");
                    });
                });
        }
    }
}
