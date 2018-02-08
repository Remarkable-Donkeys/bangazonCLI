using System;

namespace bangazonCLI
{
    public static class AddSellProductInterface
    {
        //Create an instance of the ProductManager 
        static ProductManager prodManager = new ProductManager();

        //Method that will display prompts to get required data for a new product.
        public static void Show()
        {
            //Create an empty product to fill with the user input.
            Product _newProduct = new Product();
            //Set the customerId to the Active Customer
            _newProduct.CustomerId =1;


            //Prompt the user to input the required data for the product and add it to the corresponding property of the new Product.
            Console.WriteLine("Product Name:");
            Console.Write("> ");
            _newProduct.Name = Console.ReadLine();

            Console.WriteLine("Product Description:");
            Console.Write("> ");
            _newProduct.Description = Console.ReadLine();

            Console.WriteLine("Product Price");
            Console.Write("> ");
            _newProduct.Price = Double.Parse(Console.ReadLine());

            Console.WriteLine("Product Quantity:");
            Console.Write("> ");
            _newProduct.Quantity = Int32.Parse(Console.ReadLine());

            //use the ProductManager to ADD the product to the database.
            prodManager.Add(_newProduct);

            //Run the ProductSellMenu Show method to take the user back to the Product Menu.
            ProductSellMenu.DisplayMenu();
            
            
        }
    }
}
