using ShopsRUs.Api.Enums;
using System;

namespace ShopsRUs.Api.Entities
{
	public class Customer
	{
        public int id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public CustomerType Type { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
