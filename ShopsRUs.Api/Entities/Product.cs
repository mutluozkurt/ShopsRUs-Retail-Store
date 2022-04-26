using ShopsRUs.Api.Enums;
using System;

namespace ShopsRUs.Api.Entities
{
    public class Product
    {
        public string Name { get; set; }

        public ProductType Type { get; set; }

        public decimal Price { get; set; }
    }
}
