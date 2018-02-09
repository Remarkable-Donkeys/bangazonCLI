using System;

namespace bangazonCLI
{
    public class ProductSellMenu
    {
        private int _activeCustomer = CustomerManager.ActiveCustomerId;
        public static int Show()
        {

            Console.Clear();
            Console.WriteLine("Add Products to Sell");
            Console.WriteLine("**********************");
            Console.WriteLine("1. Add New Product");
            Console.WriteLine("2. Update a Product");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. Return to Customer Menu");
            Console.Write("> ");
            int userChoice = Int32.Parse(Console.ReadLine());

            return userChoice;
        }

        public static void DisplayMenu()
        {
            int choice = Show();
            if (choice <= 4 && choice != 0)
            {
                switch (choice)
                {
                    case 1:

                        AddSellProductInterface.Show();
                        break;

                    case 2:

                        UpdateSellProductInterface.Show();
                        break;

                    case 3:

                        DeleteSellProductInterfcae.Show();
                        break;

                    case 4:

                        break;
                }
            } else {
                DisplayMenu();
            }
        }
    }
}