using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
    public class ProductManager
    {

        DatabaseInterface db;
        public ProductManager(string DBenvironment)
        {
            db = new DatabaseInterface(DBenvironment);
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
            Product returnedProduct = new Product();
            string sql = $"SELECT * FROM Product WHERE Product.Id = {productId}";
            db.Query(sql, (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    returnedProduct.Id = reader.GetInt32(0);
                    returnedProduct.Name = reader[1].ToString();
                    returnedProduct.Description = reader[2].ToString();
                    returnedProduct.Price = reader.GetDouble(3);
                    returnedProduct.Quantity = reader.GetInt32(5);
                    returnedProduct.CustomerId = reader.GetInt32(4);
                    returnedProduct.DateAdded = reader[6].ToString();

                }
            });
            return returnedProduct;
        }

        public void Update(int productId, int customerId, Product updatedProduct)
        {
            string sql = $"UPDATE Product SET Name = '{updatedProduct.Name}', Description = '{updatedProduct.Description}', Price = '{updatedProduct.Price}', Quantity = '{updatedProduct.Quantity}' WHERE Id= {productId} AND CustomerId = {customerId}";

            db.Update(sql);
        }

        public void Delete(int productId, int customerId)
        {
            string sql = $"DELETE FROM Product WHERE Id = {productId} AND CustomerId = {customerId}";

            db.Update(sql);
        }

    }
}