using System;
using System.Collections.Generic;
using System.Linq;

namespace bangazonCLI
{
    public class ProductManager
    {
        //private List to hold all products
        private List<Product> _productTable = new List<Product>();
        
        //method to ADD a product to the system
        public void Add(Product newProduct){
            _productTable.Add(newProduct);
        }

        // method to GET ALL products in the system
        public List<Product> GetAllProducts(){
            return _productTable;
        }
        //method to GET a single product that matches the given productId
        public Product GetSingleProduct(int producId)
        {
            return _productTable.Where(p => p.Id == producId).Single();
          
        }

    }
}