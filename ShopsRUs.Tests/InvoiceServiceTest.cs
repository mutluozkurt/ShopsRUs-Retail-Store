using FluentAssertions;
using Moq;
using ShopsRUs.Api.Controllers;
using ShopsRUs.Api.Dtos;
using ShopsRUs.Api.Entities;
using ShopsRUs.Api.Enums;
using ShopsRUs.Api.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShopsRUs.Tests
{
    public class InvoiceServiceTest
    {
        private readonly InvoiceService invoiceService = new();

        [Fact]
        public async Task GetInvoice_WithEmployee_ReturnEmployeeInvoice()
        {
            //Arrange
            CustomerType customerType = CustomerType.Employee;
            Customer customer = CreateCustomer(customerType);
            List<Product> products = CreateProduct();

            var expectedItem = new Invoice()
            {
                id = Guid.NewGuid(),
                Customer = customer,
                Total = 340.00M,
                Discount = 87.000M,
                NetAmountPayable = 253.000M,
                createdDate = DateTime.Now,
            };

            //Act
             var result = await invoiceService.CalculateInvoiceAsync(customer,products);

            //Assert
            result.Should().BeEquivalentTo(
                expectedItem,
                options => options.Excluding(ex => ex.id).Excluding(ex => ex.createdDate)
                );
            result.id.Should().NotBeEmpty();
            result.createdDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1000));
        }

        [Fact]
        public async Task GetInvoice_WithAffiliate_ReturnAffiliateInvoice()
        {
            //Arrange
            CustomerType customerType = CustomerType.Affiliate;
            Customer customer = CreateCustomer(customerType);
            List<Product> products = CreateProduct();

            var expectedItem = new Invoice()
            {
                id = Guid.NewGuid(),
                Customer = customer,
                Total = 340.00M,
                Discount = 39.000M,
                NetAmountPayable = 301.000M,
                createdDate = DateTime.Now,
            };

            //Act
            var result = await invoiceService.CalculateInvoiceAsync(customer, products);

            //Assert
            result.Should().BeEquivalentTo(
               expectedItem,
               options => options.Excluding(ex => ex.id).Excluding(ex => ex.createdDate)
               );
            result.id.Should().NotBeEmpty();
            result.createdDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1000));
        }

        [Fact]
        public async Task GetInvoice_WithRegularCustomer_ReturnRegularCustomerInvoice()
        {
            //Arrange
            CustomerType customerType = CustomerType.RegularCustomer;
            Customer customer = CreateCustomer(customerType ,new DateTime(2022, 05, 20));
            List<Product> products = CreateProduct();

            var expectedItem = new Invoice()
            {
                id = Guid.NewGuid(),
                Customer = customer,
                Total = 340.00M,
                Discount = 15.000M,
                NetAmountPayable = 325.000M,
                createdDate = DateTime.Now,
            };

            //Act
            var result = await invoiceService.CalculateInvoiceAsync(customer, products);

            //Assert
            result.Should().BeEquivalentTo(
              expectedItem,
              options => options.Excluding(ex => ex.id).Excluding(ex => ex.createdDate)
              );
            result.id.Should().NotBeEmpty();
            result.createdDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1000));
        }

        [Fact]
        public async Task GetInvoice_WithRegularCustomerOverTwoYears_ReturnRegularCustomerInvoice()
        {
            //Arrange
            CustomerType customerType = CustomerType.RegularCustomer;
            Customer customer = CreateCustomer(customerType, new DateTime(2019, 05, 20));
            List<Product> products = CreateProduct();

            var expectedItem = new Invoice()
            {
                id = Guid.NewGuid(),
                Customer = customer,
                Total = 340.00M,
                Discount = 27.000M,
                NetAmountPayable = 313.000M,
                createdDate = DateTime.Now,
            };

            //Act
            var result = await invoiceService.CalculateInvoiceAsync(customer, products);

            //Assert
            result.Should().BeEquivalentTo(
              expectedItem,
              options => options.Excluding(ex => ex.id).Excluding(ex => ex.createdDate)
              );
            result.id.Should().NotBeEmpty();
            result.createdDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1000));
        }



        private static Customer CreateCustomer(CustomerType customerType)
        {
            return new()
            {
                id = 1,
                Name = "Jon",
                Surname = "Snow",
                Type = customerType,
                CreatedAt = new DateTime(2019, 05, 20)
            };
        }

        private static Customer CreateCustomer(CustomerType customerType, DateTime createdAt)
        {
            return new()
            {
                id = 1,
                Name = "Jon",
                Surname = "Snow",
                Type = customerType,
                CreatedAt = createdAt
            };
        }

        private static List<Product> CreateProduct()
        {
            List<Product> products = new();
            products.Add(new Product()
            {
                Name = "Cheese",
                Price = 100.00M,
                Type = ProductType.Groceries
            });
            products.Add(new Product()
            {
                Name = "Phone",
                Price = 40.00M,
                Type = ProductType.Electronics
            });
            products.Add(new Product()
            {
                Name = "Shoes",
                Price = 70.00M,
                Type = ProductType.Sports
            });
            products.Add(new Product()
            {
                Name = "T-shirt",
                Price = 130.00M,
                Type = ProductType.Clothing
            });
         
            return products;
        }
    }
}
