using ShopsRUs.Api.Entities;
using System;
using System.Collections.Generic;

namespace ShopsRUs.Api.Dtos
{
    public class InvoiceRequest
    {
        public int customerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
