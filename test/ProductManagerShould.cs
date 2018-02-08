using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using bangazonCLI;


namespace bangazonCLI.Test
{


    public class BangazonCLI_Should
    {
        DatabaseInterface db = new DatabaseInterface();
        [Fact]
        public void AddProduct()
        {

            db.NukeDB();
            db.CheckDatabase();

            db.Insert($@"
            INSERT INTO Customer
            (Id, FirstName, LastName, DateCreated)
            VALUES
            (null, 'Tyler', 'Bowman', '2018-01-01')");

            ProductManager manager = new ProductManager();

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
            db.Insert($@"
            INSERT INTO Customer
            (Id, FirstName, LastName, DateCreated)
            VALUES
            (null, 'Tyler', 'Bowman', '2018-01-01')");

            ProductManager manager = new ProductManager();
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
            db.Insert($@"
            INSERT INTO Customer
            (Id, FirstName, LastName, DateCreated)
            VALUES
            (null, 'Tyler', 'Bowman', '2018-01-01')");

            ProductManager manager = new ProductManager();
            Product _product = new Product("Necklace", "A necklace", 58.23, 1);
            _product.CustomerId = 1;
            int newId = manager.Add(_product);

            Product returnedProduct = manager.GetSingleProduct(newId);



            Assert.Equal(returnedProduct.Id, newId);



        }
    }
}

