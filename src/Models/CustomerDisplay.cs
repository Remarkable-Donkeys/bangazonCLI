using System;
using System.Collections.Generic;

namespace bangazonCLI
{
    public class CustomerDisplay
    {
        private DatabaseInterface _db = new DatabaseInterface();
        public CustomerDisplay(CustomerManager manager)
        {
            Console.Clear();
            Console.WriteLine("To return to main menu enter 0");
            Console.WriteLine("*************************************************");
            Console.WriteLine("Please enter Customer Id of the active customer");
            List<Customer> currentCustomers = manager.GetAllCustomers();
            foreach (Customer c in currentCustomers)
            {
                Console.WriteLine(" Id: " + c.Id + "    Name: " + c.FirstName + " " + c.LastName + "    Phone: " + c.Phone);
            }
            Console.WriteLine(">");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            int activeId = int.Parse(enteredKey.KeyChar.ToString());

            //if user selects 0 return to the main menu
            if (activeId == 0)
            {
                MainMenu.Show();
            }
            else
            {
                //user selects a customer to set as active
                manager.SetActive(activeId);

                //update the active customer LastActive Date with today's date
                string activeDate = DateTime.Now.ToString();
                _db.Update($@"UPDATE Customer
                    SET LastActive = '{activeDate}'
                    WHERE Id = {activeId};");                
            }



        }
    }
}