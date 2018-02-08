using System;
using System.Collections.Generic;
using System.Linq;

namespace bangazonCLI
{
    public static class DeleteSellProductInterfcae
    {
        static ProductManager ProdManager = new ProductManager();

        public static void Show()
        {
            int ActiveCustomerId = 1;
            List<Product> AllProducts = ProdManager.GetAllProducts();
            List<Product> CustomerProducts = AllProducts.Where(p => p.CustomerId == ActiveCustomerId).ToList();

            if (CustomerProducts.Count > 0)
            {
                Dictionary<int, int> productId = new Dictionary<int, int>();
                int i = 1;
                Console.WriteLine("Which product would you like to update?");
                //Loop through the list of products and write them to the console.
                foreach (Product p in CustomerProducts)
                {
                    Console.WriteLine($"{i}. {p.Name}");
                    productId.Add(i, p.Id);

                    i += 1;
                }

                Console.Write("> ");
                int choice = Int32.Parse(Console.ReadLine());
                if (choice <= i && choice != 0)
                {
                    int prodId = productId[choice];
                    Product prodToDelete = ProdManager.GetSingleProduct(prodId);


                    Console.WriteLine($"Are you sure you want to DELETE {prodToDelete.Name}?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    Console.Write("> ");

                    int userChoice = Int32.Parse(Console.ReadLine());
                    if (userChoice <= 2 && userChoice != 0)
                    {
                        switch (userChoice)
                        {
                            case 1:
                                ProdManager.Delete(prodId, ActiveCustomerId);
                                ProductSellMenu.DisplayMenu();
                                break;
                            case 2:
                                ProductSellMenu.DisplayMenu();
                                break;
                        }

                    }
                    else
                    {
                        Show();
                    }
                }
                else
                {
                    Show();
                }
            }
        }



    }
}