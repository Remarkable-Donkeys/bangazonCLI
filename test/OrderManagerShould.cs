
using System;
using System.Collections.Generic;
using Xunit;

namespace bangazonCLI.Tests
{
    public class OrderManagerShould
    {
        // Class Variables
		private Product _product;
        private Order _order;

		// OrderManager is to manage all existing orders in the db
        private OrderManager _orderManager;
		private int _activeCustomerId;

        // Constructor
		public OrderManagerShould()
		{
			_activeCustomerId = 1;
			// building Product
			_product = new Product(1, "Book", "A book", 25.55, 2);

			// building the Order
			_order = new Order();

            // order properties
            _order.Id = 1;
            _order.CustomerId = _activeCustomerId;
            _order.DateCreated = DateTime.Now;
            _order.PaymentTypeId = 1;
            _order.DateOrdered = DateTime.Now;
			
            // AddProduct will add product to a list of type product in order object
            _order.AddProduct(_product);


			_orderManager = new OrderManager();

            // order manager properties
            // order products list

            // I think I don't need the following here
            // _orderManager.AddOrder();
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
            _order.AddProduct(_product);

            List<Product> orderProductList = _order.GetProductList();
            
            Assert.Contains(_product, orderProductList);
        }

        // Is product in order, based on product id
        [Fact]
        public void IsProductInOrder()
        {
            Boolean isProductInOrder = _order.IsProductInOrder(_product);

            Assert.Equal(false, isProductInOrder);

            _order.AddProduct(_product);

            isProductInOrder = _order.IsProductInOrder(_product);

            Assert.Equal(true, isProductInOrder);
        }

        // Get single product from order
        [Fact]
        public void GetSingleProductFromOrder()
        {
            int productId = 1;
            _order.AddProduct(_product);

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
            _orderManager.AddOrder(_order);

            List<Order> orderList = _orderManager.GetOrderList();

            Boolean isOrderInOrderManager = orderList.Contains(_order);

            Assert.Equal(true, isOrderInOrderManager);
        }

        // Return list of all orders tracked by order manager
        [Fact]
        public void ListOrdersInOrderManager()
        {
            _orderManager.AddOrder(_order);

            List<Order> orderList = _orderManager.GetOrderList();

            Assert.Contains(_order, orderList);

        }

        // Is order in order manager, based on order id
        [Fact]
        public void IsOrderInOrderManager()
        {
            Boolean isOrderInOrderManager = _orderManager.IsOrderInOrderManager(_order);

            Assert.Equal(false, isOrderInOrderManager);

            _orderManager.AddOrder(_order);

            isOrderInOrderManager = _order.IsOrderInOrderManager(_order);

            Assert.Equal(true, isOrderInOrderManager);
        }

        // get single order from order manager
        public void GetSingleOrderFromOrderManager()
        {
            int orderId = 1;
            _orderManager.AddOrder(_order);

            Order order = _orderManager.GetOrder(orderId);

            Assert.Equal(order, _order);
        }

        
    }
}