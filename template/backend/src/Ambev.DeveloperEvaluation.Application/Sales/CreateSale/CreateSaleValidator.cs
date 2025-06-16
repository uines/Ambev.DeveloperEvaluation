using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using FluentValidation;
 
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for Sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(command => command.Customer)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

        RuleFor(command => command.Branch)
            .MaximumLength(100).WithMessage("Branch name cannot exceed 100 characters."); // Pode ser opcional

        RuleFor(command => command.Products)
            .NotEmpty().WithMessage("At least one product is required for the sale.")
            .ForEach(productRule => productRule.SetValidator(new ProductSaleDtoCommandValidator()));
    }
}

/// <summary>
/// Validator for ProductSaleDTO within a CreateSaleCommand.
/// </summary>
public class ProductSaleDtoCommandValidator : AbstractValidator<CreateProductSaleDTO>
{
    public ProductSaleDtoCommandValidator()
    { // Renomeado para evitar conflito de nome se ProductSaleDto fosse usado diretamente
        RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Quantity).GreaterThan(0);
        RuleFor(p => p.Quantity).LessThanOrEqualTo(20).WithMessage("It's not possible to sell above 20 identical items.");
        RuleFor(p => p.UnitPrice).GreaterThan(0);
    }
}