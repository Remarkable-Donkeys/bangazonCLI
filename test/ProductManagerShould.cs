using System;
using Xunit;

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
    }
}
