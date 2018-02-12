using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
    public class StaleProducts
    {
            private DatabaseInterface _db;
            private ProductManager _pManager;
            private OrderManager _oManager;
            private Order _order;
            private List<Product> _productList;
            private List<Order> _orderList;
            private List<Product> _allOrderedProducts;
            private List<Product> _staleProducts;

        public StaleProducts(string databaseEnvironment){
            _db = new DatabaseInterface(databaseEnvironment);
            _pManager = new ProductManager(databaseEnvironment);
            _oManager = new OrderManager(databaseEnvironment);
            _order = new Order(databaseEnvironment);

            _productList = _pManager.GetAllProducts();
            //all orders in system
            _orderList = _oManager.GetOrderList();

            //all ordered products
            _allOrderedProducts = new List<Product>();
            //stale products list
            _staleProducts = new List<Product>();

            //product stale date
            DateTime pStaleDate = DateTime.Now.AddDays(-180);
            //order stale date
            DateTime oStaleDate = DateTime.Now.AddDays(-90);



            //requirement 1
            foreach(Order o in _orderList)
            {
                o.GetProductList().ForEach(p => {
                    if (!_allOrderedProducts.Contains(p))
                        {
                        //add products to the stale products list
                            _allOrderedProducts.Add(p);
                        }
                });
            }    
            foreach(Product p in _productList)
            {
                // adds products that have been in the system over 180 days and never added to an order
                if(p.DateAdded < pStaleDate && !_allOrderedProducts.Contains(p))
                {
                    _staleProducts.Add(p);
                }
            }        

            //requirement 2
            foreach (Order o in _orderList)
            {
                //if an order is incomplete and over 90 days old
                if (o.DateOrdered == null && o.DateCreated < oStaleDate)
                {
                    //get list of products for each stale order
                    o.GetProductList().ForEach(p =>
                    {
                        if (!_staleProducts.Contains(p))
                        {
                        //add products to the stale products list
                            _staleProducts.Add(p);
                        }

                    });
                }
            }

            //requirement 3
            foreach (Order o in _orderList)
            {
                //if an order has been completed
                if (o.DateOrdered != null)
                {
                    o.GetProductList().ForEach(p =>
                        {
                            //if the product was added over 180 days ago, has a quantity greater than 0 and is not already in the staleProducts list
                            if (p.DateAdded < pStaleDate && p.Quantity > 0 && !_staleProducts.Contains(p))
                            {
                                //add product to staleProducts list
                                _staleProducts.Add(p);
                            }

                        });
                }
            }
        }

        public void Show(){
            Console.Clear();
            Console.WriteLine("To return to main menu enter 0");
            Console.WriteLine("*************************************************");
            Console.WriteLine("Stale Products");

            int i = 1;
            foreach(Product p in _staleProducts){
                Console.WriteLine(i + " " + p.Name);
                i += i;
            }


            //if user presses 0 go back to main menu, if any other number, stay on stale item menu
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            int exit = int.Parse(enteredKey.KeyChar.ToString());
            if(exit == 0){
                return;
            } else {
                Show();
            }
        }

    }
}