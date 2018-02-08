/*author:   Kristen Norris
purpose:    Display the Customer Menu
methods:    Show: shows the Customer Menu in the console (without functionality)
            DisplayMenu: switch statment to give functionality to the Customer Menu
 */
using System;

namespace bangazonCLI
{
    public class CustomerMenu
    {
        public static int Show()
        {
            //creates the active customer menu for the Bangazon Interface
            Console.Clear();
            Console.WriteLine("*************************************************");
            Console.WriteLine("Active Customer Menu");
            Console.WriteLine("*************************************************");
            Console.WriteLine("1. Create a payment option");
            Console.WriteLine("2. Selling Product Menu");
            Console.WriteLine("3. Purchasing Product Menu");
            Console.WriteLine("4. Show revenue report");
            Console.WriteLine("5. Return to Main Menu!");
            Console.Write("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            return int.Parse(enteredKey.KeyChar.ToString());
        }

        public static void DisplayMenu()
        {
            int choice2 = Show();

            switch (choice2)
            {
                case 1:
                    //Add Payment Type
                    break;
                case 2:
                    //Go to Selling Product Menu
                    break;
                case 3:
                    //Go to Purchasing Product Menu
                    break;
                case 4:
                    //Show Revenue Report
                    break;
                case 5:
                    //reset ActiveCustomerId 
                    CustomerManager.ActiveCustomerId = 0;
                    //return to Main Menu
                    return;
            }
        }

    }
}