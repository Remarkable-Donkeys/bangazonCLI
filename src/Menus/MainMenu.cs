/*author:   Kristen Norris
purpose:    Show Main Menu
methods:    Show: show the main menu in the console
 */
using System;

namespace bangazonCLI
{
    public class MainMenu
    {
        public static int Show()
        {
            //creates the main menu for the Bangazon Interface
            Console.Clear();
            Console.WriteLine ("*************************************************");
            Console.WriteLine ("Welcome to Bangazon! Command Line Ordering System");
            Console.WriteLine ("*************************************************");
            Console.WriteLine ("1. Create a customer account");
            Console.WriteLine ("2. Choose active customer ");
            Console.WriteLine ("3. Show stale products");
            Console.WriteLine ("4. Show overall product popularity");
            Console.WriteLine ("5. Leave Bangazon!");
            Console.Write ("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            return int.Parse(enteredKey.KeyChar.ToString());
        }
    }
}