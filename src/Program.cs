using System;
using System.Collections.Generic;

namespace bangazonCLI
{
    class Program
    {
        static void Main(string[] args)

        {
            string environment = "BANGAZONCLI";
            DatabaseInterface db = new DatabaseInterface(environment);


            CustomerManager cManager = new CustomerManager(environment);

            db.CheckDatabase();

            int choice1;

            do
            {
                choice1 = MainMenu.Show();

                switch (choice1)
                {
                    case 1:
                        //create a new customer
                        CustomerInput.New(cManager);
                        
                        break;
                    case 2:
                        //choose active customer
                        CustomerActive.SelectCurrent(cManager, db);
                        
                        break;
                    case 3:
                        //stale items
                        StaleProducts stale = new StaleProducts(environment);
                        stale.Show();
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