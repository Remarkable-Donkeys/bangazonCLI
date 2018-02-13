using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Data.Sqlite;
using System.Collections;

namespace bangazonCLI.Tests
{
    public class StaleProductsShould
    {
        string dbE = "BANGAZONTEST";
        private DatabaseInterface _db;
        private ProductManager _pManager;
        private OrderManager _oManager;
        private CustomerManager _cManager;
        private PaymentTypeManager _payManager;
        private Order _order1;
        private Order _order2;

        //constructor
        public StaleProductsShould(){
            _db = new DatabaseInterface(dbE);
            _pManager = new ProductManager(dbE);
            _oManager = new OrderManager(dbE);
            _cManager = new CustomerManager(dbE);
            _payManager = new PaymentTypeManager(dbE);
            _order1 = new Order(dbE);
            _order2 = new Order(dbE);

            _db.CheckDatabase();

            Customer bob = new Customer(){
                FirstName = "Bob",
                LastName = "Jones",
                Address = "200 Jackson Lane",
                City= "Nashville",
                State= "TN",
                PostalCode= "12345",
                Phone="123-123-1234",
                DateCreated= DateTime.Now,
                LastActive=DateTime.Now
            };
            Customer amber = new Customer(){
                FirstName = "Amber",
                LastName = "Andrews",
                Address = "200 Galaxy Way",
                City= "Nashville",
                State= "TN",
                PostalCode= "12345",
                Phone="123-123-1234",
                DateCreated= DateTime.Now,
                LastActive=DateTime.Now
            };
            //bob is seller
            int CustId1 = _cManager.Add(bob);
            //amber is purchaser
            int CustId2 = _cManager.Add(amber);
            //payment type for amber
            PaymentType _payment = new PaymentType(CustId2, "VISA", "1234567");
            int payA = _payManager.AddPaymentType(_payment);

            //added over 180 days ago and never added to an order
            Product _product1 = new Product("Jeans", "A pair of js", 45.32, 1);
            _product1.CustomerId = CustId1;
            _product1.DateAdded = DateTime.Now.AddDays(-200);
            _product1.Id = _pManager.Add(_product1);

            //added to a completed order but still has quantity
            Product _product2 = new Product("Necklace", "A necklace", 58.23, 4);
            _product2.CustomerId = CustId1;
            _product2.DateAdded = DateTime.Now.AddDays(-220);
            _product2.Id = _pManager.Add(_product2);

            //added to an incomplete order and the order was created over 90 days ago
            Product _product3 = new Product("Clothes", "These are clothes", 5.23, 4);
            _product3.CustomerId = CustId1;
            _product3.DateAdded = DateTime.Now.AddDays(-160);
            _product3.Id = _pManager.Add(_product3);

            //added to the system less than 180 days ago
            Product _product4 = new Product("Movie", "A movie", 8.00, 5);
            _product4.CustomerId = CustId1;
            _product4.DateAdded = DateTime.Now.AddDays(-50);
            _product4.Id = _pManager.Add(_product4);

            //amber's completed order
            _order1.CustomerId = CustId2; //customer id
			_order1.DateCreated = DateTime.Now.AddDays(-200); //created 200 days ago
            _order1.Id = _oManager.AddOrder(_order1); //add order to database
            _order1.AddProduct(_product2); //add product to order
            _order1.DateOrdered = DateTime.Now.AddDays(-199); //order completed 199 days ago
            _oManager.CompleteOrder(_order1.Id, payA); //complete order added to database

            //amber's incomplete order
            _order2.CustomerId = CustId2; //customer id
			_order2.DateCreated = DateTime.Now.AddDays(-120); //created 120 days ago
            _order2.Id = _oManager.AddOrder(_order2); //add order to database
            _order2.AddProduct(_product3); //add product to order
        }


        [Fact]
        public void OldOrders()
        {
            StaleProducts stale = new StaleProducts(dbE);

            Assert.Equal(3, StaleProducts.StaleProductsList.Count);

        }
    }
}
