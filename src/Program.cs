using System;

namespace bangazonCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseInterface db = new DatabaseInterface();

            CustomerManager cManager = new CustomerManager();

            db.CheckDatabase();

            int choice1;

            do
            {
                choice1 = MainMenu.Show();

                switch (choice1)
                {
                    case 1:
                        //new customer
                        CustomerInput newCustomer = new CustomerInput(cManager);
                        CustomerMenu.DisplayMenu();
                        break;
                    case 2:
                        //choose active customer
                        CustomerDisplay currentCustomer = new CustomerDisplay(cManager);
                        CustomerMenu.DisplayMenu();
                        break;
                    case 3:
                        //stale items
                        break;
                    case 4:
                        //popular items
                        break;
                }
            }
            while (choice1 != 5);
    }
    }
}