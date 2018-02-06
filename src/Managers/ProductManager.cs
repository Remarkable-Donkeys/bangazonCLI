using System;
using System.Collections.Generic;

namespace bangazonCLI
{
    public class ProductManager
    {
        private List<Product> _productTable = new List<Product>();

        public void Add(Product newProduct){
            _productTable.Add(newProduct);
        }
    }
}