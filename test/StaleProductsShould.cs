using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Data.Sqlite;
using System.Collections;

namespace bangazonCLI.Tests
{
    public class StaleProductsShould
    {
        private DatabaseInterface _db = new DatabaseInterface("BANGAZONCLI");
        private ProductManager _pManager = new ProductManager("BANGAZONTEST");
        private CustomerManager _cManager = new CustomerManager("BANGAZONTEST");

        Customer bob = new Customer()
        {
            FirstName = "Bob",
            LastName = "Jones",
            Address = "200 Jackson Lane",
            City = "Nashville",
            State = "TN",
            PostalCode = "12345",
            Phone = "123-123-1234",
            DateCreated = DateTime.Now,
            LastActive = DateTime.Now
        };


        [Fact]
        public void OldProducts()
        {
            int cId = _cManager.Add(bob);

            Product _p1 = new Product()
            {
                Name = "Book",
                Description = "this is a book",
                Price = 10.99,
                Quantity = 3,
                DateAdded = DateTime.Now.AddDays(-300),
                CustomerId = cId
            };
            Product _p2 = new Product()
            {
                Name = "Bat",
                Description = "this is a Bat",
                Price = 10.99,
                Quantity = 3,
                DateAdded = DateTime.Now,
                CustomerId = cId
            };
            Product _p3 = new Product()
            {
                Name = "Movie",
                Description = "this is a Movie",
                Price = 1.99,
                Quantity = 3,
                DateAdded = DateTime.Now.AddDays(-100),
                CustomerId = cId
            };

            _pManager.Add(_p1);
            _pManager.Add(_p2);
            _pManager.Add(_p3);
            //products that have been in the system more than 180 days
            List<Product> productList = _pManager.GetAllProducts();

            DateTime staleDate = DateTime.Now.AddDays(-180);

            List<Product> staleProduct = new List<Product>();

            foreach (Product p in productList)
            {
                //covert string to datetime
                DateTime pDate = Convert.ToDateTime(p.DateAdded);
                if (pDate < staleDate)
                {
                    staleProduct.Add(p);
                }
            }

            Assert.Equal(1, staleProduct.Count);

        }

        [Fact]
        public void OldOrders()
        {
            ICollection all;
            List<Product> staleProducts = new List<Product>();

            _db.Query($@"SELECT p.Name AS 'Product', p.DateAdded, p.Quantity, op.OrderId, o.DateCreated, o.DateOrdered
            FROM Product p
            LEFT JOIN OrderedProduct op
            ON p.Id = op.ProductId
            LEFT JOIN `Order` o
            ON op.OrderId = o.Id",
            (SqliteDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.Id = reader.GetInt32(0);

                            all.Add()

                        }
                    }
                    );


            //product in system 180days

            //never added to order or added to order with remaining quantity

            //added to order that wasn't completed
            //order created over 90days ago

            /*
            SELECT p.Name AS 'Product', p.DateAdded, p.Quantity, op.OrderId, o.DateCreated, o.DateOrdered
            FROM Product p
            LEFT JOIN OrderedProduct op
            ON p.Id = op.ProductId
            LEFT JOIN `Order` o
            ON op.OrderId = o.Id
            */
        }
    }
}