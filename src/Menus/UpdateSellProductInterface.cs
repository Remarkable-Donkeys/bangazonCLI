using System;
using System.Collections.Generic;
using System.Linq;

namespace bangazonCLI
{
    public static class UpdateSellProductInterface
    {
        static ProductManager prodManager = new ProductManager();

        public static void Show()
        {
            //GET all products from the database.
            List<Product> AllProducts = prodManager.GetAllProducts();

            //Filter out only the active customer's products.
            List<Product> ProductList = AllProducts.Where(p => p.CustomerId == 1).ToList();

            //If there are products in the list give them the option to update.
            if (ProductList.Count > 0)
            {
                Dictionary<int, int> productId = new Dictionary<int, int>();
                int i = 1;
                Console.WriteLine("Which product would you like to update?");
                //Loop through the list of products and write them to the console.
                foreach (Product p in ProductList)
                {
                    Console.WriteLine($"{i}. {p.Name}");
                    productId.Add(i, p.Id);
        
                    i+=1;
                }

                Console.Write("> ");
                int userChoice = Int32.Parse(Console.ReadLine());
                if (userChoice <= i && userChoice != 0)
                {
                //store the user's choice in a variable
                int ProdId = productId[userChoice];
                //get the corresponding product from the database.
                Product ProdToUpdate = prodManager.GetSingleProduct(ProdId);
                    displayUpdate(ProdToUpdate, ProdId);
                } else {
                    Show();
                }
                
                //run this method to allow them to update one or more of the product's properties.
                void displayUpdate(Product product, int id)
                {
                    Console.Clear();

                    Console.WriteLine($"What property of {product.Name} would you like to update?");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Description");
                    Console.WriteLine("3. Price");
                    Console.WriteLine("4. Quantity");
                    Console.WriteLine("5. Finished Updating");
                    Console.Write("> ");

                    int choice = Int32.Parse(Console.ReadLine());
                    //if the user has selected a valid choice do what they selected.
                    if(choice <= 5 && choice != 0)
                    {
                    switch (choice)
                    {
                        case 1:

                            Console.WriteLine("What is the new name?");
                            Console.Write("> ");
                            product.Name = Console.ReadLine();
                            displayUpdate(product, id);
                            break;

                        case 2:

                            Console.WriteLine("What is the new description?");
                            Console.Write("> ");
                            product.Description = Console.ReadLine();
                            displayUpdate(product, id);
                            break;

                        case 3:

                            Console.WriteLine("What is the new price?");
                            Console.Write("> ");
                            product.Price = double.Parse(Console.ReadLine());
                            displayUpdate(product, id);
                            break;

                        case 4:

                            Console.WriteLine("What is the new quantity?");
                            Console.Write("> ");
                            product.Quantity = Int32.Parse(Console.ReadLine());
                            displayUpdate(product, id);
                            break;

                        case 5:

                            prodManager.Update(id, 1, product);
                            ProductSellMenu.DisplayMenu();
                            break;

                    }
                    //if they selected a choice that is invalid return them to the update product menu.
                } else {
                    displayUpdate(product, id);
                }
                }
                

            }
            //If the active customer has no products inform them and return to the product menu.
            else
            {
                Console.WriteLine("No products found for current customer.");
                ProductSellMenu.DisplayMenu();
            }
        }
    }
}