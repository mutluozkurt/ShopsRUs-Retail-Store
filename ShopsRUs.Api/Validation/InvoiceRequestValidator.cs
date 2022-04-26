using FluentValidation;
using ShopsRUs.Api.Dtos;
using ShopsRUs.Api.Entities;

namespace ShopsRUs.Api.Validation
{
    public class InvoiceRequestValidator : AbstractValidator<InvoiceRequest>
    {
        public InvoiceRequestValidator()
        {
            //CustomerId not empty
            RuleFor(c => c.customerId).NotEmpty().WithMessage("Please do not leave the user type blank.");
            //CustomerId must be greater than 0
            RuleFor(c => c.customerId).GreaterThan(0).WithMessage("CustomerId must be greater than 0");
            //Products not empty
            RuleFor(c => c.Products).NotEmpty().WithMessage("Please do not leave the product infromation blank.");
            //Bill items count must greater tahan 0
            RuleFor(c => c.Products.Count).GreaterThan(0).WithMessage("Please do not leave the product infromation blank.");
            //Bill items detail validation
            RuleForEach(c => c.Products).SetValidator(new ProductValidator());

        }

        public class ProductValidator : AbstractValidator<Product>
        {
            public ProductValidator()
            {
                //Product type must be defined and not empty
                RuleFor(c => c.Type).NotEmpty().WithMessage("Please do not leave the product type blank.").IsInEnum().WithMessage("Please enter a defined product type.");
                //Product price not empty and greater than 0
                RuleFor(c => c.Price).NotEmpty().WithMessage("Please do not leave the product price blank.").GreaterThan(0).WithMessage("Please enter a valid price.");
            }
        }
    }
}
