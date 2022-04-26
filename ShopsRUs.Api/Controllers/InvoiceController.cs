using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Api.Dtos;
using ShopsRUs.Api.Entities;
using ShopsRUs.Api.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopsRUs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("GetInvoice")]
        public async Task<ActionResult<Invoice>> GetInvoice(InvoiceRequest request)
        {

            Customer customer = await _invoiceService.GetCustomer(request.customerId);
            
            if (customer is null)
            {
                string message = "Customer not found";
                return NotFound(message);
            }

            List<Product> products = request.Products;

            Invoice result = await _invoiceService.CalculateInvoiceAsync(customer,products);

            return  result;
        }
    }
}
