using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(request => request.Branch)
            .NotEmpty().WithMessage("Branch name is required.");

        RuleFor(request => request.Products)
            .NotEmpty().WithMessage("At least one product is required for the sale.")
            .ForEach(productRule => productRule.SetValidator(new ProductSaleDtoRequestValidator()));
    }
}

/// <summary>
/// Validator for ProductSaleDTO within a CreateSaleRequest.
/// </summary>
public class ProductSaleDtoRequestValidator : AbstractValidator<CreateProductSale>
{
    public ProductSaleDtoRequestValidator() {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.").MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Product quantity must be greater than zero.");
        RuleFor(p => p.Quantity).LessThanOrEqualTo(20).WithMessage("It's not possible to sell above 20 identical items.");
        RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Product unit price must be greater than zero.");
    }
}