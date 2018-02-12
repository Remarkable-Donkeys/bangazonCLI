
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace bangazonCLI.Tests
{
    public class OrderManagerShould
    {
        /*******************/
        /* Class Variables */
        /*******************/
		private Product _product;
        private Order _order;

		// OrderManager is to manage all existing orders in the db
        private OrderManager _orderManager;
        private CustomerManager _customerManager;
		private int _activeCustomerId;
        private string DBenvironment = "BANGAZONTEST";
        private DatabaseInterface _db;


        /***************/
        /* Constructor */
        /***************/
		public OrderManagerShould()
		{
            _db = new DatabaseInterface(DBenvironment);
			_activeCustomerId = 1;
			_product = new Product("Book", "A book", 25.55, 2);
            _product.CustomerId = _activeCustomerId;

			_order = new Order(DBenvironment);

            _order.CustomerId = _activeCustomerId;
            _order.DateCreated = DateTime.Now;

			_orderManager = new OrderManager(DBenvironment);
            _customerManager = new CustomerManager(DBenvironment);
		}

        /*******************************/
        /* Order Manager Class Methods */
        /*******************************/

        // Add order to order manager
        [Fact]
        public void AddToOrderManager()
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

			_order.CustomerId = _customerManager.Add(bob);

            int orderId = _orderManager.AddOrder(_order);

            List<Order> orderList = _orderManager.GetOrderList();

            Boolean isOrderInOrderManager = orderId == _orderManager.GetSingleOrder(orderId).Id;

            Assert.True(isOrderInOrderManager);

            _db.Update($"DELETE FROM `Order` WHERE Id={orderId}");

			_db.Update($"DELETE FROM `Customer` WHERE Id={_order.CustomerId}");
        }

        // Remove order from order manager
        [Fact]
        public void RemoveOrderFromOrderManager()
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

            _order.CustomerId = _customerManager.Add(bob);

            int orderId = _orderManager.AddOrder(_order);

            Boolean orderInOrderManager = orderId == _orderManager.GetSingleOrder(orderId).Id;

            Assert.True(orderInOrderManager);

            _orderManager.RemoveOrder(orderId);

            orderInOrderManager = orderId == _orderManager.GetSingleOrder(orderId).Id;

            Assert.False(orderInOrderManager);

            _db.Update($"DELETE FROM `Order` WHERE Id={orderId}");

            _db.Update($"DELETE FROM `Customer` WHERE Id={_order.CustomerId}");
        }

        // Return list of all orders tracked by order manager
        [Fact]
        public void ListOrdersInOrderManager()
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

            _order.CustomerId = _customerManager.Add(bob);

            int orderId = _orderManager.AddOrder(_order);

            List<Order> orderList = _orderManager.GetOrderList();

            Assert.True(orderList.Count > 0);

            _db.Update($"DELETE FROM `Order` WHERE Id={orderId}");

            _db.Update($"DELETE FROM `Customer` WHERE Id={_order.CustomerId}");
        }

        // Removes all orders from order manager
        [Fact]
        public void RemoveAllOrdersFromOrderManager()
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

            _order.CustomerId = _customerManager.Add(bob);

            int orderId = _orderManager.AddOrder(_order);

            List<Order> orderList = _orderManager.GetOrderList();

            Assert.True(orderList.Count > 0);

            _orderManager.RemoveAllOrders();

            orderList = _orderManager.GetOrderList();

            Assert.Equal(0, orderList.Count);

            _db.Update($"DELETE FROM `Customer` WHERE Id={_order.CustomerId}");
        }

        // Is order in order manager, based on order id
        [Fact]
        public void IsOrderInDatabase()
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

            _order.CustomerId = _customerManager.Add(bob);

            int orderId = _orderManager.AddOrder(_order);

            Boolean isOrderInOrderManager = _orderManager.IsOrderInDatabase(orderId);

            Assert.True(isOrderInOrderManager);

            _db.Update($"DELETE FROM `Order` WHERE Id={orderId}");

            _db.Update($"DELETE FROM `Customer` WHERE Id={_order.CustomerId}");
        }

        // get single order from order manager
        [Fact]
        public void GetSingleOrderFromOrderManager()
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

            _order.CustomerId = _customerManager.Add(bob);

            int orderId = _orderManager.AddOrder(_order);
            
            Order order = _orderManager.GetSingleOrder(orderId);

            Assert.Equal(order.Id, orderId);
            Assert.Equal(order.CustomerId, _order.CustomerId);

            _db.Update($"DELETE FROM `Order` WHERE Id={orderId}");

            _db.Update($"DELETE FROM `Customer` WHERE Id={_order.CustomerId}");
        }

        [Fact]
        public void CompleteOrder()
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

            _order.CustomerId = _customerManager.Add(bob);

            PaymentType _paymentType = new PaymentType(_order.CustomerId, "VISA", "1234567");

            PaymentTypeManager _paymentTypeManager = new PaymentTypeManager(DBenvironment);
            
            int paymentId = _paymentTypeManager.AddPaymentType(_paymentType);
            
            int orderId = _orderManager.AddOrder(_order);

            _orderManager.CompleteOrder(orderId, paymentId);

            Order order = _orderManager.GetSingleOrder(orderId);

            Boolean paymentIdMatch = order.PaymentTypeId == paymentId;
            
            Assert.NotNull(order.DateOrdered);
            Assert.True(paymentIdMatch);

            _db.Update($"DELETE FROM `Order` WHERE Id={orderId}");
            _db.Update($"DELETE FROM `PaymentType` WHERE Id={paymentId}");
            _db.Update($"DELETE FROM `Customer` WHERE Id={_order.CustomerId}");
        }
    }
}