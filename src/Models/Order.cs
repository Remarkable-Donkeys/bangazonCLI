using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
    public class Order
    {
        /*******************/
        /* Class Variables */
        /*******************/
        private static DatabaseInterface _db = new DatabaseInterface();
        private List<Product> _productList = new List<Product>();

        /********************/
        /* Class Properties */
        /********************/
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public DateTime DateCreated { get; set; }
		public int? PaymentTypeId { get; set; }
		// date ordered is the date of the order is closed
        public DateTime? DateOrdered { get; set; }


        /***************/
        /* Constructor */
        /***************/
        public Order(){
            _productList = new List<Product>();
        }

        /*****************/
        /* Class Methods */
        /*****************/
        public int AddProduct(Product product)
        {
            // _productList.Add(product);
            return _db.Insert($@"
				INSERT INTO `OrderedProduct`
				(`Id`, `ProductId`, `OrderId`)
				VALUES
				    (null, 
                    (SELECT Id FROM Product Where Id={product.Id}), 
                    (SELECT Id FROM `Order` WHERE Id={Id}))
			");
        }
        public List<Product> GetProductList()
        {
            //selects customer information from the database and adds it to a List<Customer>
            _db.Query($@"
                SELECT p.Id, p.Name, p.Description, p.Price, p.CustomerId, p.Quantity, p.DateAdded 
                FROM Product p, OrderedProduct op 
                WHERE op.OrderId = {Id}",
            (SqliteDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            
							product.Id = reader.GetInt32(0);
							product.Name = reader[1].ToString();
                            product.Description = reader[2].ToString();
                            product.Price = reader.GetDouble(3);
                            product.CustomerId = reader.GetInt32(4);
                            product.Quantity = reader.GetInt32(5);
                            product.DateAdded = reader.GetDateTime(6).ToString();

                            _productList.Add(product);
                        }
                    });

            return _productList;
        }
        
        public Product GetSinglerProductFromOrder(int productId)
        {
			Product product = new Product();
			
            _db.Query($@"SELECT `Id`, `Name`, `Description`, `Price`, `CustomerId`, `Quantity`, `DateAdded` FROM `Product`",
				(SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {                        
                        product.Id = reader.GetInt32(0);
                        product.Name = reader[1].ToString();
                        product.Description = reader[2].ToString();
                        product.Price = reader.GetDouble(3);
                        product.CustomerId = reader.GetInt32(4);
                        product.Quantity = reader.GetInt32(5);
                        product.DateAdded = reader.GetDateTime(6).ToString();
                    }
                });

            return product;
		}
	}
}