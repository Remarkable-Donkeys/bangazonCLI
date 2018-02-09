using System;
using System.Collections.Generic;

namespace bangazonCLI
{
    public class StaleProducts
    {
        private static ProductManager _pManager = new ProductManager("BANGAZONCLI");

        public static void OldProducts()
        {
            //products that have been in the system more than 180 days
            List<Product> productList = _pManager.GetAllProducts();

            DateTime staleDate = DateTime.Now.AddDays(-180);

            List<Product> staleProduct = new List<Product>();

            foreach (Product p in productList)
            {
                //covert string to datetime
                DateTime pDate = Convert.ToDateTime(p.DateAdded);
                if (pDate < staleDate)
                {
                    staleProduct.Add(p);
                }
            }

        }

    }
}