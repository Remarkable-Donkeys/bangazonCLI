using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using bangazonCLI;


namespace bangazonCLI.Test
{


    public class BangazonCLI_Should
    {
        DatabaseInterface db = new DatabaseInterface("BANGAZONTEST");
        ProductManager manager = new ProductManager("BANGAZONTEST");
        CustomerManager custManager = new CustomerManager();
        [Fact]
        public void AddProduct()
        {

            db.NukeDB();
            db.CheckDatabase();

            
            Customer tyler = new Customer("Tyler", "Bowman");
            custManager.Add(tyler);



            Product _product = new Product("Book", "A BOOK", 25.55, 2);
            _product.CustomerId = 1;
            int newId = manager.Add(_product);
            var returnedProduct = manager.GetSingleProduct(newId);

            Assert.Equal("Book", returnedProduct.Name);
            Assert.Equal("A BOOK", returnedProduct.Description);
            Assert.Equal(25.55, returnedProduct.Price);
            Assert.Equal(2, returnedProduct.Quantity);
            Assert.Equal(1, returnedProduct.CustomerId);

        }

        [Fact]
        public void GetProducts()
        {
            db.NukeDB();
            db.CheckDatabase();
            Customer tyler = new Customer("Tyler", "Bowman");
            custManager.Add(tyler);

            Product _product = new Product("Shirt", "A shirt", 35.43, 5);
            _product.CustomerId = 1;
            int newId = manager.Add(_product);
            _product.Id = newId;
            List<Product> allProducts = manager.GetAllProducts();
            Assert.Equal(1, allProducts.Count());
        }

        [Fact]

        public void GetSingleProduct()
        {

            db.NukeDB();
            db.CheckDatabase();
            Customer tyler = new Customer("Tyler", "Bowman");
            custManager.Add(tyler);

            Product _product = new Product("Necklace", "A necklace", 58.23, 1);
            _product.CustomerId = 1;
            int newId = manager.Add(_product);

            Product returnedProduct = manager.GetSingleProduct(newId);



            Assert.Equal(returnedProduct.Id, newId);



        }

        [Fact]

        public void UpdateProduct()
        {
            db.NukeDB();
            db.CheckDatabase();
            Customer tyler = new Customer("Tyler", "Bowman");
            custManager.Add(tyler);

            Product _product = new Product("Jeans", "A pair of js", 45.32, 1);
            _product.CustomerId = 1;

            int newId = manager.Add(_product);
            _product.Description = "A pair of JEANS";

            manager.Update(newId, 1, _product);

            Product updatedProduct = manager.GetSingleProduct(newId);

            Assert.Equal(updatedProduct.Description, "A pair of JEANS");

        }

        [Fact]

        public void DeleteProduct()
        {
            db.NukeDB();
            db.CheckDatabase();
            Customer tyler = new Customer("Tyler", "Bowman");
            custManager.Add(tyler);

            Product _product = new Product("Jeans", "A pair of js", 45.32, 1);
            _product.CustomerId = 1;
            Product _product2 = new Product("Necklace", "A necklace", 58.23, 1);
            _product2.CustomerId = 1;

            int newId = manager.Add(_product);
            manager.Add(_product2);

            manager.Delete(newId, 1);

            List<Product> AllProducts = manager.GetAllProducts();

            Assert.Equal(1, AllProducts.Count());
            Assert.Equal("Necklace", AllProducts[0].Name);


        }
    }
}

