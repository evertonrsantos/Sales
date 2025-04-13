using FluentValidation;
namespace SalesApi.Application.Commands.Validators
{
    public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
    {
        public CancelSaleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale ID is required");
        }
    }
}