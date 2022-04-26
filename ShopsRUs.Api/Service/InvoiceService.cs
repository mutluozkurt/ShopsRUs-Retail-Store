using ShopsRUs.Api.Dtos;
using ShopsRUs.Api.Entities;
using ShopsRUs.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Api.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly List<Customer> customers = new()
        {
            new Customer { id = 1, Name = "Christopher", Surname = "Garcia", Type= CustomerType.Employee, CreatedAt = DateTime.Now },
            new Customer { id = 2, Name = "Cindy ", Surname = "Holmes", Type = CustomerType.Affiliate, CreatedAt = DateTime.Now },
            new Customer { id = 3, Name = "Thomas ", Surname = "Clark", Type = CustomerType.RegularCustomer, CreatedAt = DateTime.Now },
            new Customer { id = 4, Name = "Kevin", Surname = "Powell", Type = CustomerType.RegularCustomer, CreatedAt = DateTime.Now.AddYears(-3) },
        };

        public async Task<Invoice> CalculateInvoiceAsync(Customer customer, List<Product> products)
        {

            double discountPercentageValue = GetDiscountPercentage(customer);
            decimal allItemTotal = 0;
            decimal withoutGroceryTotal = 0;
            decimal discount = 0;
            decimal grandTotal = 0;

            foreach (var product in products)
            {
                if (product.Type != ProductType.Groceries)
                    withoutGroceryTotal += product.Price;
                
                allItemTotal += product.Price;
            }

            decimal percantageDiscount = withoutGroceryTotal * (decimal)discountPercentageValue;
            decimal generalDiscount = (allItemTotal / 100) >= 1 ? (Convert.ToInt32(Math.Floor(allItemTotal / 100)) * 5) : 0;

            discount = percantageDiscount + generalDiscount;
            grandTotal = allItemTotal - discount;

            var result = new Invoice
            {
                id = Guid.NewGuid(),
                Customer = customer,
                Total = allItemTotal,
                Discount = discount,
                NetAmountPayable = grandTotal,
                createdDate = DateTime.Now,
            };

            return await Task.FromResult(result);
        }

        private static double GetDiscountPercentage(Customer customer)
        {
            CustomerType customerType = customer.Type;
            bool isOverTwoYears = customer.CreatedAt.HasValue && (DateTime.Now > customer.CreatedAt.Value.AddYears(2));
            double percentage = 0;

            switch (customerType)
            {
                case CustomerType.Employee:
                    percentage = 0.30;
                    break;
                case CustomerType.Affiliate:
                    percentage = 0.10;
                    break;
                case CustomerType.RegularCustomer:
                    if (isOverTwoYears)
                        percentage = 0.05;
                    break;
                default:
                    break;
            }

            return percentage;
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            Customer customer = customers.SingleOrDefault(c => c.id == customerId);

            if (customer is null)
                return null;

            return await Task.FromResult(customer);
        }
    }
}
