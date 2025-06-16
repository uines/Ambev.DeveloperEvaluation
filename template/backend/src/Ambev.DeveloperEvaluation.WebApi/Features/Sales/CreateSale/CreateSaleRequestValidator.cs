using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(request => request.Customer)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

        RuleFor(request => request.Branch)
            .MaximumLength(100).WithMessage("Branch name cannot exceed 100 characters.");

        RuleFor(request => request.Products)
            .NotEmpty().WithMessage("At least one product is required for the sale.")
            .ForEach(productRule => productRule.SetValidator(new ProductSaleDtoRequestValidator()));
    }
}

/// <summary>
/// Validator for ProductSaleDTO within a CreateSaleRequest.
/// </summary>
public class ProductSaleDtoRequestValidator : AbstractValidator<CreateProductSaleDTO>
{
    public ProductSaleDtoRequestValidator() {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Quantity).GreaterThan(0);
        RuleFor(p => p.Quantity).LessThanOrEqualTo(20).WithMessage("It's not possible to sell above 20 identical items.");
        RuleFor(p => p.UnitPrice).GreaterThan(0);
    }
}