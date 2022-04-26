
# ShopRUs

ShopsRUs is an existing retail outlet. They would like to provide discount to their customers on all their web/mobile platforms. They require a set of APIs to be built that provide capabilities to calculate discounts, generate the total costs and generate the invoices for customers

The following discounts apply:

1.If the user is an employee of the store, he gets a 30% discount

2.If the user is an affiliate of the store, he gets a 10% discount

3.If the user has been a customer for over 2 years, he gets a 5% discount.

4.For every $100 on the bill, there would be a $ 5 discount (e.g. for $ 990, you get $ 45 as a discount).

5.The percentage based discounts do not apply on groceries.

6.A user can get only one of the percentage based discounts on a bill.



## Run Solution
To run the project, simply download the project and run it in visual studio 2019+ or VS Code.


## Usage/Examples
In the application, four different customers are kept in memory.

```c#
private readonly List<Customer> customers = new()
        {
            new Customer { id = 1, Name = "Christopher", Surname = "Garcia", Type= CustomerType.Employee, CreatedAt = DateTime.Now },
            new Customer { id = 2, Name = "Cindy ", Surname = "Holmes", Type = CustomerType.Affiliate, CreatedAt = DateTime.Now },
            new Customer { id = 3, Name = "Thomas ", Surname = "Clark", Type = CustomerType.RegularCustomer, CreatedAt = DateTime.Now },
            new Customer { id = 4, Name = "Kevin", Surname = "Powell", Type = CustomerType.RegularCustomer, CreatedAt = DateTime.Now.AddYears(-3) },
        };
```

You can test on these four users using the sample data below.You can test it with different customer types by changing the CustomerId.

```
{
  "customerId":1,
  "products": [
    {
      "type": 1,
      "price": 100.00
    },{
      "type": 2,
      "price": 50.00
    },{
      "type": 3,
      "price": 80.00
    },{
      "type": 4,
      "price": 120.00
    }
  ]
}


```