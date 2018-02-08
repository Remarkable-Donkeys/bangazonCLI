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
            Console.WriteLine("**************************************");
            Console.WriteLine("1. Add New Product");
            Console.WriteLine("2. Update a Product");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. Return to Customer Menu");
            Console.Write("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            return int.Parse(enteredKey.KeyChar.ToString());
        }
    }
}