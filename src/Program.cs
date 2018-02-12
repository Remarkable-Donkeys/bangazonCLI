using System;

namespace bangazonCLI
{
    class Program
    {
        static void Main(string[] args)

        {
            DatabaseInterface db = new DatabaseInterface("BANGAZONCLI");


            CustomerManager cManager = new CustomerManager("BANGAZONCLI");

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