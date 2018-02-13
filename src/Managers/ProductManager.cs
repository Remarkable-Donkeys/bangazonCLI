/*author:   Tyler Bowman
purpose:    Handle database interactions with Products
methods:    Add Product
            Get All Products
            Get Single Product
            Update Product
            Delete Product
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
    public class ProductManager
    {

        DatabaseInterface db;
        OrderManager orderManager ;
        public ProductManager(string DBenvironment)
        {
            db = new DatabaseInterface(DBenvironment);
           orderManager = new OrderManager(DBenvironment);
        }
        private int _activeCustomerId = 1;


        //method to ADD a product to the system
        public int Add(Product newProduct)
        {
            string sql = $"insert into Product (Id, Name, Description, Price, Quantity, CustomerId, DateAdded) values (null, '{newProduct.Name}', '{newProduct.Description}', {newProduct.Price}, {newProduct.Quantity}, {newProduct.CustomerId}, '2018-01-01')";

            int newId = db.Insert(sql);
            return newId;
        }

        // method to GET ALL products in the system
        public List<Product> GetAllProducts()
        {
            List<Product> AllProducts = new List<Product>();
            string sql = $"SELECT * FROM Product";
            db.Query(sql, (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    Product returnedProduct = new Product();

                    returnedProduct.Id = reader.GetInt32(0);
                    returnedProduct.Name = reader[1].ToString();
                    returnedProduct.Description = reader[2].ToString();
                    returnedProduct.Price = reader.GetDouble(3);
                    returnedProduct.Quantity = reader.GetInt32(5);
                    returnedProduct.CustomerId = reader.GetInt32(4);
                    returnedProduct.DateAdded = reader[6].ToString();

                    AllProducts.Add(returnedProduct);
                }
            });

            return AllProducts;
        }
        //method to GET a single product that matches the given productId
        public Product GetSingleProduct(int productId)
        {
            //instantiates a new product.
            Product returnedProduct = new Product();
            string sql = $"SELECT * FROM Product WHERE Product.Id = {productId}";
            //runs DB query
            db.Query(sql, (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    //Takes the returned data from the database and assign it to the appropriate properties of the new Product.
                    returnedProduct.Id = reader.GetInt32(0);
                    returnedProduct.Name = reader[1].ToString();
                    returnedProduct.Description = reader[2].ToString();
                    returnedProduct.Price = reader.GetDouble(3);
                    returnedProduct.Quantity = reader.GetInt32(5);
                    returnedProduct.CustomerId = reader.GetInt32(4);
                    returnedProduct.DateAdded = reader[6].ToString();

                }
            });
            //returns the new product.
            return returnedProduct;
        }

        public void Update(int productId, int customerId, Product updatedProduct)
        {
            string sql = $"UPDATE Product SET Name = '{updatedProduct.Name}', Description = '{updatedProduct.Description}', Price = '{updatedProduct.Price}', Quantity = '{updatedProduct.Quantity}' WHERE Id= {productId} AND CustomerId = {customerId}";

            db.Update(sql);
        }

        public void Delete(int productId, int customerId)
        {
            //Gets a list of all orders in the database
            List<Order> allOrders = orderManager.GetOrderList();
            //boolean indicating whether or not the given product is attached to an order.  Default is set to false.
            bool productOrdered = false;
            //Iterate over the list of orders
            foreach (Order o in allOrders)
            {
                //for each Order in the list, store its list of ordered products.
                List<Product> orderedProducts = o.GetProductList();
                //Iterate over the list of ordered products
                foreach (Product p in orderedProducts)
                {
                    //Conditional statement that checks to see if the Id of the ordered product matches the Id of the product to delete.
                    if (p.Id == productId)
                    {
                        //if it is equal, set the boolean to true
                        productOrdered = true;
                    }
                }
            }
            //Conditional statement that says if the product is not in an order go ahead and delete it from the database.
            if (productOrdered == false)
            {
                string sql = $"DELETE FROM Product WHERE Id = {productId} AND CustomerId = {customerId}";
                db.Update(sql);
            } else 
            {
                //if it is in one or more order, write an error message to the console saying it cannot be deleted.
                Console.WriteLine("Product has been added to an order, cannot delete.");
            }

        }

    }
}