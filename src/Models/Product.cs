using System;

namespace bangazonCLI
{
    public class Product
    {
        public int Id {get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateAdded { get; set; }

        //Constructor that generates a test Product for development purposes.
        public Product(){
            Id = 1;
            Name = "Test";
            Description = "Test";
            Price = 25.00;
            Quantity = 1;
            CustomerId = 1;
            DateAdded = DateTime.Today;
        }

        //Constructor that takes arguments for CustomerId, Name, Description, Price, and Quantity.
        public Product(int customerId, string name, string description, double price, int quantity)
        {
            Id = 1;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            CustomerId = customerId;
            DateAdded = DateTime.Today;

        }
    }
}