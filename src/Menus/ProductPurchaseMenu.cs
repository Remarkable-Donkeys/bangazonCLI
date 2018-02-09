/*author:   Kristen Norris
purpose:    Display the Purchase Product Menu
methods:    Show: shows the Purchase Product Menu in the console (without functionality)
            DisplayMenu: switch statment to give functionality to the Purchase Product Menu
 */
using System;

namespace bangazonCLI
{
    public class ProductPurchaseMenu
    {
        public static int Show()
        {

            Console.Clear();
            Console.WriteLine("Purchase Products");
            Console.WriteLine("**********************");
            Console.WriteLine("1. Add product to shopping cart");
            Console.WriteLine("2. Complete an order");
            Console.WriteLine("3. Return to Customer Menu");
            Console.Write("> ");
            int userChoice = Int32.Parse(Console.ReadLine());

            return userChoice;
        }

        public static void DisplayMenu()
        {
            int choice = Show();
            if (choice <= 3 && choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        //add product to order
                        break;

                    case 2:
                        //complete an order
                        break;

                    case 3:
                        //return to customer menu
                        CustomerMenu.DisplayMenu();
                        break;
                }
            } else {
                DisplayMenu();
            }
        }
    }
}