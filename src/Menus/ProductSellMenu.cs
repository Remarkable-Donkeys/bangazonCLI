using System;

namespace bangazonCLI
{
    public class ProductSellMenu
    {
        private int _activeCustomer = CustomerManager.ActiveCustomerId;
        public void Show()
        {
            
            Console.Clear();
            Console.WriteLine("Add Products to Sell");
            Console.WriteLine("**************************************");
            Console.WriteLine("1. Add New Product");
            Console.WriteLine("2. Update a Product");
            Console.WriteLine("3. Delete Product");
        }
    }
}