
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
		private int _activeCustomerId;

        /***************/
        /* Constructor */
        /***************/
		public OrderManagerShould()
		{
			_activeCustomerId = 1;
			_product = new Product("Book", "A book", 25.55, 2);
            _product.CustomerId = _activeCustomerId;

			_order = new Order();

            _order.CustomerId = _activeCustomerId;
            _order.DateCreated = DateTime.Now;

			_orderManager = new OrderManager();
		}

        /***********************/
        /* Order Class Methods */
        /***********************/

        // Add product to order and list products from order
        [Fact]
        public void AddProductToOrder()
        {
            _product.CustomerId = 1;
            ProductManager productdManager = new ProductManager();
            _product.Id = productdManager.Add(_product);

            _order.Id = _orderManager.AddOrder(_order);

            _order.AddProduct(_product);

            List<Product> orderProductList = _order.GetProductList();
            
            Product productInDb = orderProductList.Where(p => p.Id == _product.Id).Single();
            
            Assert.Equal(_product.Id, productInDb.Id);
            Assert.Equal(_product.Name, productInDb.Name);
            Assert.Equal(_product.Description, productInDb.Description);
            Assert.Equal(_product.Price, productInDb.Price);
            Assert.Equal(_product.CustomerId, productInDb.CustomerId);
            Assert.Equal(_product.Quantity, productInDb.Quantity);
            // Assert.Equal(_product.DateAdded, productInDb.DateAdded);
        }

        /*******************************/
        /* Order Manager Class Methods */
        /*******************************/

        // TODO order manager tests passing except CompleteOrder()

        // Add order to order manager
        [Fact]
        public void AddToOrderManager()
        {
            int orderId = _orderManager.AddOrder(_order);

            List<Order> orderList = _orderManager.GetOrderList();

            Boolean isOrderInOrderManager = orderId == _orderManager.GetSingleOrder(orderId).Id;

            Assert.True(isOrderInOrderManager);
        }

        // Remove order from order manager
        [Fact]
        public void RemoveOrderFromOrderManager()
        {
            int orderId = _orderManager.AddOrder(_order);

            Boolean orderInOrderManager = orderId == _orderManager.GetSingleOrder(orderId).Id;

            Assert.True(orderInOrderManager);

            _orderManager.RemoveOrder(orderId);

            orderInOrderManager = orderId == _orderManager.GetSingleOrder(orderId).Id;

            Assert.False(orderInOrderManager);
        }

        // Return list of all orders tracked by order manager
        [Fact]
        public void ListOrdersInOrderManager()
        {
            if(_orderManager.GetOrderList().Count < 1)
            {
                _orderManager.AddOrder(_order);
            }

            List<Order> orderList = _orderManager.GetOrderList();

            Assert.True(orderList.Count > 0);

        }

        // Removes all orders from order manager
        [Fact]
        public void RemoveAllOrdersFromOrderManager()
        {   
            _orderManager.RemoveAllOrders();

            List<Order> orderList = _orderManager.GetOrderList();

            Assert.Equal(0, orderList.Count);
        }

        // Is order in order manager, based on order id
        [Fact]
        public void IsOrderInDatabase()
        {
            if(_orderManager.GetOrderList().Count > 0)
            {
                _orderManager.RemoveAllOrders();
            }

            int orderId = _orderManager.AddOrder(_order);

            Boolean isOrderInOrderManager = _orderManager.IsOrderInDatabase(orderId);

            Assert.True(isOrderInOrderManager);
        }

        // get single order from order manager
        [Fact]
        public void GetSingleOrderFromOrderManager()
        {
            // int orderId = _order.Id;

            if(_orderManager.GetOrderList().Count > 0)
            {
                _orderManager.RemoveAllOrders();
            }
            
            int orderId = _orderManager.AddOrder(_order);
            
            Order order = _orderManager.GetSingleOrder(orderId);

            Assert.Equal(order.Id, orderId);
            Assert.Equal(order.CustomerId, _order.CustomerId);
        }

        [Fact]
        public void CompleteOrder()
        {
            int _paymentId = 1;
            int orderId = _orderManager.AddOrder(_order);

            _orderManager.CompleteOrder(orderId, _paymentId);

            Order order = _orderManager.GetSingleOrder(orderId);

            Boolean paymentIdMatch = order.PaymentTypeId == _paymentId;
            
            Assert.NotNull(order.DateOrdered);
            Assert.True(paymentIdMatch);
        }
    }
}