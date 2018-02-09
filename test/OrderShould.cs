
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.Data.Sqlite;

namespace bangazonCLI.Tests
{
    public class OrderShould
    {
        /*******************/
        /* Class Variables */
        /*******************/
        private Order _order;

		// OrderManager is to manage all existing orders in the db
        private OrderManager _orderManager;
		private CustomerManager _customerManager;
		private ProductManager _productManager;
		private Product _product;
		private string DBenvironment;
		private DatabaseInterface _db;

        /***************/
        /* Constructor */
        /***************/
		public OrderShould()
		{
			DBenvironment = "BANGAZONTEST";
			
			_order = new Order(DBenvironment);

			_orderManager = new OrderManager(DBenvironment);
			_customerManager = new CustomerManager(DBenvironment);
			_productManager = new ProductManager(DBenvironment);
			
			_db = new DatabaseInterface(DBenvironment);

		}

        /***********************/
        /* Order Class Methods */
        /***********************/

        // Add product to order and list products from order
        [Fact]
        public void AddProductToOrder()
        {
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

			int customerId = _customerManager.Add(bob);
			_product = new Product("Book", "The book", 2.99, 1);
			_product.CustomerId = customerId;
            
			_product.Id = _productManager.Add(_product);

			_order.CustomerId = customerId;
			_order.DateCreated = DateTime.Now;

            _order.Id = _orderManager.AddOrder(_order);

            int orderedProductId = _order.AddProduct(_product);

            List<Product> orderProductList = _order.GetProductList();
            
            Product productInDb = orderProductList.Where(p => p.Id == _product.Id).Single();
            
            Assert.Equal(_product.Id, productInDb.Id);
            Assert.Equal(_product.Name, productInDb.Name);
            Assert.Equal(_product.Description, productInDb.Description);
            Assert.Equal(_product.Price, productInDb.Price);
            Assert.Equal(_product.CustomerId, productInDb.CustomerId);
            Assert.Equal(_product.Quantity, productInDb.Quantity);
            // Assert.Equal(_product.DateAdded, productInDb.DateAdded);

			// delete record from OrderedProduct
			_db.Update($"DELETE FROM `OrderedProduct` WHERE Id={orderedProductId}");
			
			// delete product
			_db.Update($"DELETE FROM `Product` WHERE Id={_product.Id}");
			
			// delete order
			_db.Update($"DELETE FROM `Order` WHERE Id={_order.Id}");
			
			// delete customer
			_db.Update($"DELETE FROM `Customer` WHERE Id={customerId}");
        }
    }
}