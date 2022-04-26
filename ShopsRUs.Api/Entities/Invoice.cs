using System;

namespace ShopsRUs.Api.Entities
{
    public class Invoice
    {
        public Guid id { get; set; }
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmountPayable { get; set; }
        public DateTimeOffset createdDate { get; set; }

    }
}
