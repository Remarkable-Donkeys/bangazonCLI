using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using bangazonCLI;


namespace bangazonCLI.Test
{


    public class BangazonCLI_Should : IDisposable
    {
        DatabaseInterface db = new DatabaseInterface("BANGAZONTEST");
        ProductManager manager = new ProductManager("BANGAZONTEST");
        CustomerManager custManager = new CustomerManager("BANGAZONTEST");
        OrderManager orderManager = new OrderManager("BANGAZONTEST");
        
        [Fact]
        public void AddProduct()
        {
            db.CheckDatabase();
            Customer tyler = new Customer("Tyler", "Bowman");
            int custId = custManager.Add(tyler);



            Product _product = new Product("Book", "A BOOK", 25.55, 2);
            _product.CustomerId = custId;
            int newId = manager.Add(_product);
            var returnedProduct = manager.GetSingleProduct(newId);

            Assert.Equal("Book", returnedProduct.Name);
            Assert.Equal("A BOOK", returnedProduct.Description);
            Assert.Equal(25.55, returnedProduct.Price);
            Assert.Equal(2, returnedProduct.Quantity);
            Assert.Equal(custId, returnedProduct.CustomerId);

        }

        [Fact]
        public void GetProducts()
        {
           db.CheckDatabase();
            Customer tyler = new Customer("Tyler", "Bowman");
            int CustId = custManager.Add(tyler);

            Product _product = new Product("Shirt", "A shirt", 35.43, 5);
            _product.CustomerId = CustId;
            int newId = manager.Add(_product);
            _product.Id = newId;
            List<Product> allProducts = manager.GetAllProducts();
            bool productExists = false;
            foreach(Product p in allProducts)
            {
                if(p.Name == "Shirt" && p.Description == "A shirt" && p.Price == 35.43 && p.CustomerId == CustId && p.Id == newId && p.Quantity == 5){
                    productExists = true;
                }
            }
            Assert.True(productExists);
        }

        [Fact]

        public void GetSingleProduct()
        {

            db.CheckDatabase();
            Customer tyler = new Customer("Tyler", "Bowman");
           int CustId = custManager.Add(tyler);

            Product _product = new Product("Necklace", "A necklace", 58.23, 1);
            _product.CustomerId = CustId;
            int newId = manager.Add(_product);

            Product returnedProduct = manager.GetSingleProduct(newId);



            Assert.Equal(returnedProduct.Id, newId);



        }

        [Fact]

        public void UpdateProduct()
        {
           db.CheckDatabase();
            Customer tyler = new Customer("Tyler", "Bowman");
            int CustId = custManager.Add(tyler);

            Product _product = new Product("Jeans", "A pair of js", 45.32, 1);
            _product.CustomerId = CustId;

            int newId = manager.Add(_product);
            _product.Description = "A pair of JEANS";

            manager.Update(newId, CustId, _product);

            Product updatedProduct = manager.GetSingleProduct(newId);

            Assert.Equal(updatedProduct.Description, "A pair of JEANS");

        }

        [Fact]

        public void DeleteProduct()
        {
           db.CheckDatabase();
           Order newOrder = new Order("BANGAZONTEST");
            Customer tyler = new Customer("Tyler", "Bowman");
           int CustId = custManager.Add(tyler);

            Product _product = new Product("Jeans", "A pair of js", 45.32, 1);
            _product.CustomerId = CustId;
            Product _product2 = new Product("Necklace", "A necklace", 58.23, 1);
            _product2.CustomerId = CustId;

            int newId = manager.Add(_product);
            manager.Add(_product2);

            newOrder.AddProduct(_product2);

            manager.Delete(newId, CustId);

            List<Product> AllProducts = manager.GetAllProducts();

            Assert.Equal(1, AllProducts.Count());
            Assert.Equal("Necklace", AllProducts[0].Name);


        }

      

        

        void IDisposable.Dispose()
        {
            db.NukeDB();
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BangazonCLI_Should() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.

    }
}

