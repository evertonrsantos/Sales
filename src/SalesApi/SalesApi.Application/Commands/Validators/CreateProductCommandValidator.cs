using FluentValidation;

namespace SalesApi.Application.Commands.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        }
    }
}
