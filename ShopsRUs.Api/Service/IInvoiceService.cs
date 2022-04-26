using ShopsRUs.Api.Dtos;
using ShopsRUs.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopsRUs.Api.Service
{
    public interface IInvoiceService
    {
        Task<Invoice> CalculateInvoiceAsync(Customer customer, List<Product> products);
        Task<Customer> GetCustomer(int customerId);
    }
}
