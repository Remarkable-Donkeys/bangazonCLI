// /*author:   Sean Williams
// purpose:    Revenue Report Unit Tests
// Tests:    	
//  */
 

using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Xunit;
using bangazonCLI;

namespace bangazonCLI.Test
{
    public class RevenueReportManagerShould
    {
        private RevenueReportManager _manager;
        private Order _order;
        private DatabaseInterface db;
		private Customer _testCustomer;

        private OrderManager _orderManager;

		private CustomerManager _customerManager;
        private ProductManager _productManager;

        public RevenueReportManagerShould()
        {
			db = new DatabaseInterface("BANGAZONTEST");
            _manager = new RevenueReportManager("BANGAZONTEST");
			_testCustomer = new Customer("Sean", "Williams");
			_customerManager = new CustomerManager("BANGAZONTEST");
			_orderManager = new OrderManager("BANGAZONTEST");
            _productManager = new ProductManager("BANGAZONTEST");
             _order = new Order("BANGAZONTEST");
        }

        [Fact]

        public void GetProductsDictionary()
        {
            db.NukeDB();
            db.CheckDatabase();
            _order.DateCreated = DateTime.Now;
            _order.CustomerId = _customerManager.Add(_testCustomer);
            Product _product = new Product("Book", "A BOOK", 25.55, 2);
            Product _product2 = new Product("Book2", "A BOOK", 50.5, 2);
            _product.CustomerId = _order.CustomerId;
            _product.Id =  _productManager.Add(_product);
            _product2.Id =  _product2.CustomerId = _order.CustomerId;
            _product2.Id = _productManager.Add(_product2);

            _order.Id = _orderManager.AddOrder(_order);
            _order.AddProduct(_product);
            _order.AddProduct(_product);
            _order.AddProduct(_product);
            _order.AddProduct(_product);
            _order.AddProduct(_product2);

            Dictionary<string,(int, double)> productDictionay = _manager.GetProductsDictionary(_order);
            double res = 0.0;

            foreach (KeyValuePair<string, (int, double)> item in productDictionay)
            {
                res += productDictionay[item.Key].Item2;
            }
            // res/2 because res = the price of all products + the total price
            // so res/2 = total price
            Assert.Equal(152.7, res/2);
            Assert.Equal(152.7, productDictionay["Total"].Item2);
            Assert.Equal(5, productDictionay["Total"].Item1);
            Assert.Equal(102.2, productDictionay[_product.Id.ToString()].Item2);
        }

        [Fact]

        public void GetPopularItems()
        {
            db.NukeDB();
            db.CheckDatabase();

            _order.DateCreated = DateTime.Now;
            _order.CustomerId = _customerManager.Add(_testCustomer);
            Product _product = new Product("Book", "A BOOK", 25.55, 2);
            Product _product2 = new Product("Book2", "A BOOK", 50.5, 2);
            Product _product3 = new Product("Book3", "A BOOK", 50.5, 2);
            Product _product4 = new Product("Book4", "A BOOK", 50.5, 2);

            _product.CustomerId = _order.CustomerId;
            _product.Id =  _productManager.Add(_product);

            _product2.Id =  _product2.CustomerId = _order.CustomerId;
            _product2.Id =  _productManager.Add(_product2);

            _product3.Id =  _product3.CustomerId = _order.CustomerId;
            _product3.Id =  _productManager.Add(_product3);

            _product4.Id =  _product4.CustomerId = _order.CustomerId;
            _product4.Id =  _productManager.Add(_product4);
            
            _order.Id = _orderManager.AddOrder(_order);
            _order.AddProduct(_product);
            _order.AddProduct(_product);
            _order.AddProduct(_product);
            _order.AddProduct(_product);
            _order.AddProduct(_product2);
            _order.AddProduct(_product2);
            _order.AddProduct(_product3);
            _order.AddProduct(_product3);
            _order.AddProduct(_product3);
            _order.AddProduct(_product4);

            _orderManager.AddOrder(_order);
            _orderManager.AddOrder(_order);
            _orderManager.AddOrder(_order);
            _orderManager.AddOrder(_order);
            _orderManager.AddOrder(_order);
            _orderManager.AddOrder(_order);

            List<Order> orderList = _orderManager.GetOrderList();

            Dictionary<string, (int, int, double)> popularItems = _manager.GetPopularItems(orderList);

            Assert.Equal(6, popularItems[_product.Id.ToString()].Item1);
            Assert.Equal(1, popularItems[_product.Id.ToString()].Item2);
            Assert.Equal(613.2, popularItems[_product.Id.ToString()].Item3);

            Assert.Equal(6, popularItems[_product2.Id.ToString()].Item1);
            Assert.Equal(1, popularItems[_product2.Id.ToString()].Item2);
            Assert.Equal(606, popularItems[_product2.Id.ToString()].Item3);

            Assert.Equal(6, popularItems[_product3.Id.ToString()].Item1);
            Assert.Equal(1, popularItems[_product3.Id.ToString()].Item2);
            Assert.Equal(909, popularItems[_product3.Id.ToString()].Item3);

            Assert.Equal(18, popularItems["Total"].Item1);
            Assert.Equal(3, popularItems["Total"].Item2);
            Assert.Equal(2128.2, popularItems["Total"].Item3);

        }
    }
}

