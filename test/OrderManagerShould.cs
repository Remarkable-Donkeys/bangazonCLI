
using System;
using System.Collections.Generic;
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

            // order properties
            _order.Id = 1;
            _order.CustomerId = _activeCustomerId;
            _order.DateCreated = DateTime.Now;

			_orderManager = new OrderManager();
		}

        /***********************/
        /* Order Class Methods */
        /***********************/

        // Add product to order
        [Fact]
        public void AddProductToOrder()
        {
			_order.AddProduct(_product);

            List<Product> orderProductList = _order.GetProductList();
            
            Assert.Contains(_product, orderProductList);
        }

        // List all products in an order
        [Fact]
        public void ListProductsInOrder()
        {
            if(_order.GetProductList().Count < 1)
            {
                _order.AddProduct(_product);
            }

            List<Product> orderProductList = _order.GetProductList();
            
            Assert.Contains(_product, orderProductList);
        }

        // Remove all products from order
        [Fact]
        public void RemoveAllProductsFromOrder()
        {
            if(_order.GetProductList().Count < 1)
            {
                _order.AddProduct(_product);
            }

            List<Product> orderProductList = _order.GetProductList();
            
            Assert.Contains(_product, orderProductList);

            _order.RemoveAllProducts();

            Boolean isProductCountGreaterThanZero = _order.GetProductList().Count > 0;

            Assert.False(isProductCountGreaterThanZero);
        }

        // Is product in order, based on product id
        [Fact]
        public void IsProductInOrder()
        {
            if(_order.GetProductList().Count > 0)
            {
                _order.RemoveAllProducts();
            }

            List<Product> orderProductList = _order.GetProductList();

            Boolean isProductInOrder = _order.IsProductInOrder(_product);

            Assert.False(isProductInOrder);

            _order.AddProduct(_product);

            isProductInOrder = _order.IsProductInOrder(_product);

            Assert.True(isProductInOrder);
        }

        // Get single product from order
        [Fact]
        public void GetSingleProductFromOrder()
        {
            if(_order.GetProductList().Count < 1)
            {
                _order.AddProduct(_product);
            }
            int productId = _product.Id;

            Product product = _order.GetProduct(productId);

            Assert.Equal(product, _product);
        }

        /*******************************/
        /* Order Manager Class Methods */
        /*******************************/

        // Add order to order manager
        [Fact]
        public void AddToOrderManager()
        {
            int orderId = _orderManager.AddOrder(_order);

            List<Order> orderList = _orderManager.GetOrderList();

            Boolean isOrderInOrderManager = orderList.Contains(_order);

            Assert.True(isOrderInOrderManager);
        }

        // Remove order from order manager
        [Fact]
        public void RemoveOrderFromOrderManager()
        {
            int orderId = _order.Id;
            if(_orderManager.GetOrderList().Count > 0)
            {
                _orderManager.RemoveAllOrders();
            }

            _orderManager.AddOrder(_order);

            Boolean orderInOrderManager = _orderManager.GetOrderList().Contains(_order);

            Assert.True(orderInOrderManager);

            _orderManager.RemoveOrder(orderId);

            orderInOrderManager = _orderManager.GetOrderList().Contains(_order);

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

            Assert.Contains(_order, orderList);

        }

        // Removes all orders from order manager
        [Fact]
        public void RemoveAllOrdersFromOrderManager()
        {
            if(_orderManager.GetOrderList().Count > 0)
            {
                _orderManager.RemoveAllOrders();
            }
            
            List<Order> orderList = _orderManager.GetOrderList();

            Boolean orderInOrderManager = orderList.Contains(_order);

            Assert.False(orderInOrderManager);
        }

        // Is order in order manager, based on order id
        [Fact]
        public void IsOrderInOrderManager()
        {
            if(_orderManager.GetOrderList().Count > 0)
            {
                _orderManager.RemoveAllOrders();
            }
            Boolean isOrderInOrderManager = _orderManager.IsOrderInOrderManager(_order);

            Assert.False(isOrderInOrderManager);

            _orderManager.AddOrder(_order);

            isOrderInOrderManager = _orderManager.IsOrderInOrderManager(_order);

            Assert.True(isOrderInOrderManager);
        }

        // get single order from order manager
        [Fact]
        public void GetSingleOrderFromOrderManager()
        {
            int orderId = _order.Id;

            if(_orderManager.GetOrderList().Count > 0)
            {
                _orderManager.RemoveAllOrders();
            }
            
            _orderManager.AddOrder(_order);
            
            Order order = _orderManager.GetSingleOrder(orderId);

            Assert.Equal(order, _order);
        }

        [Fact]
        public void CompleteOrder()
        {
            int _paymentId = 1;
            int orderId = _order.Id;

            if(_orderManager.GetOrderList().Count > 0)
            {
                _orderManager.RemoveAllOrders();
            }
            
            _orderManager.AddOrder(_order);

            // to complete order a paymentId should be passed
            _orderManager.CompleteOrder(orderId, _paymentId);

            Order order = _orderManager.GetSingleOrder(orderId);

            Boolean validOrderDate = order.DateOrdered > order.DateCreated;
            
            Assert.True(validOrderDate);
        }
    }
}