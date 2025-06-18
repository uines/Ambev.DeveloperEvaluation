using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;
 
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for Sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(command => command.CustomerId)
            .NotEmpty().WithMessage("Customer is required.");

        RuleFor(request => request.Branch)
             .NotEmpty().WithMessage("Branch name is required.");

        RuleFor(command => command.Products)
            .NotEmpty().WithMessage("At least one product is required for the sale.")
            .ForEach(productRule => productRule.SetValidator(new ProductSaleDtoCommandValidator()));
    }
}

/// <summary>
/// Validator for ProductSaleDTO within a CreateSaleCommand.
/// </summary>
public class ProductSaleDtoCommandValidator : AbstractValidator<CreateProductSale>
{
    public ProductSaleDtoCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Product quantity must be greater than zero.");
        RuleFor(p => p.Quantity).LessThanOrEqualTo(20).WithMessage("It's not possible to sell above 20 identical items.");
        RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Product unit price must be greater than zero.");
    }
}