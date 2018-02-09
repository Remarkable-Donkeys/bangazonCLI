using System;

namespace bangazonCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseInterface db = new DatabaseInterface("BANGAZONCLI");
            
            CustomerManager cManager = new CustomerManager();

            db.CheckDatabase();
            

            ProductSellMenu.DisplayMenu();
        }
    }
}
