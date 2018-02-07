using System;
using System.Linq;
using System.Collections.Generic;

namespace bangazonCLI
{
    public class Order
    {
        /*******************/
        /* Class Variables */
        /*******************/
        private List<Product> _productList = new List<Product>();

        /********************/
        /* Class Properties */
        /********************/
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public DateTime DateCreated { get; set; }
		public int PaymentTypeId { get; set; }
		// date ordered is the date of the order is closed
        public DateTime DateOrdered { get; set; }


        /***************/
        /* Constructor */
        /***************/
        public Order(){
            _productList = new List<Product>();
        }

        /*****************/
        /* Class Methods */
        /*****************/
        public void AddProduct(Product product)
        {
            _productList.Add(product);
        }
        public List<Product> GetProductList()
        {
            return _productList;
        }
        public void RemoveAllProducts()
        {
            _productList.Clear();
        }
        public Boolean IsProductInOrder(Product product)
        {
            return _productList.Contains(product);
        }
        public Product GetProduct(int productId)
        {
            return _productList.Where(p => p.Id == productId).Single();
        }
	}
}