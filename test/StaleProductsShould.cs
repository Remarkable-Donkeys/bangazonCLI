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
        private ProductManager _pManager = new ProductManager("BANGAZONCLI");
        private OrderManager _oManager = new OrderManager("BANGAZONCLI");
        private Order _order = new Order("BANGAZONCLI");


        [Fact]
        public void OldOrders()
        {
            _db.CheckDatabase();
            //all products in system
            List<Product> productList = _pManager.GetAllProducts();
            //all orders in system
            List<Order> orderList = _oManager.GetOrderList();

            //all ordered products
            List<Product> allOrderedProducts = new List<Product>();
            //stale products list
            List<Product> staleProducts = new List<Product>();

            //product stale date
            DateTime pStaleDate = DateTime.Now.AddDays(-180);
            //order stale date
            DateTime oStaleDate = DateTime.Now.AddDays(-90);



            //requirement 1
            foreach(Order o in orderList)
            {
                o.GetProductList().ForEach(p => {
                    if (!allOrderedProducts.Contains(p))
                        {
                        //add products to the stale products list
                            allOrderedProducts.Add(p);
                        }
                });
            }    
            foreach(Product p in productList)
            {
                // adds products that have been in the system over 180 days and never added to an order
                if(p.DateAdded < pStaleDate && !allOrderedProducts.Contains(p))
                {
                    staleProducts.Add(p);
                }
            }        

            //requirement 2
            foreach (Order o in orderList)
            {
                //if an order is incomplete and over 90 days old
                if (o.DateOrdered == null && o.DateCreated < oStaleDate)
                {
                    //get list of products for each stale order
                    o.GetProductList().ForEach(p =>
                    {
                        if (!staleProducts.Contains(p))
                        {
                        //add products to the stale products list
                            staleProducts.Add(p);
                        }

                    });
                }
            }

            //requirement 3
            foreach (Order o in orderList)
            {
                //if an order has been completed
                if (o.DateOrdered != null)
                {
                    o.GetProductList().ForEach(p =>
                        {
                            //if the product was added over 180 days ago, has a quantity greater than 0 and is not already in the staleProducts list
                            if (p.DateAdded < pStaleDate && p.Quantity > 0 && !staleProducts.Contains(p))
                            {
                                //add product to staleProducts list
                                staleProducts.Add(p);
                            }

                        });
                }
            }

            Assert.Equal(4, staleProducts.Count);

        }
    }
}
