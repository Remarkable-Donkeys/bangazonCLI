using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;


namespace bangazonCLI.Test
{


    public class BangazonCLI_Should
    {

        [Fact]
        public void AddProduct()
        {

            ProductManager manager = new ProductManager();

            Product _product = new Product(1, "Book", "A book", 25.55, 2);


            Assert.Equal(_product.Name, "Book");
            Assert.Equal(_product.Description, "A book");
            Assert.Equal(_product.Price, 25.55);
            Assert.Equal(_product.Quantity, 2);
            Assert.Equal(_product.CustomerId, 1);

        }

        [Fact]
        public void GetProducts()
        {
            ProductManager manager = new ProductManager();
            Product _product = new Product(1, "Book", "A book", 25.55, 2);

            manager.Add(_product);
            List<Product> allProducts = manager.GetAllProducts();
            Assert.Contains(_product, allProducts);
        }

        [Fact]

        public void GetSingleProduct()
        {
            ProductManager manager = new ProductManager();
            Product _product = new Product(1, "Book", "A book", 25.55, 2);
            manager.Add(_product);
            int RequtestedProductIt = 1;
            Product returnedProduct = manager.GetSingleProduct(RequtestedProductIt);

            Assert.Equal(returnedProduct.Id, RequtestedProductIt);



        }
    }
}

