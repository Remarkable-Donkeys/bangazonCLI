/*author:   Kristen Norris
purpose:    Allows user to select the active customer
methods:    SelectCurrent: shows a list of all the customers in the database, when  user selects which customer should be active, it sets that customer as active and brings the user to the Customer Menu
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace bangazonCLI
{
    public class CustomerActive
    {
        public static void SelectCurrent(CustomerManager manager, DatabaseInterface db)
        {
            //display a list of all customers in the database
            Console.Clear();
            Console.WriteLine("To return to main menu enter 0");
            Console.WriteLine("*************************************************");
            Console.WriteLine("Please enter the number of the active customer");
            //list of all customers in database
            List<Customer> currentCustomers = manager.GetAllCustomers();
            //number for numbered list
            int i = 1;
            //dictionary to store the list item with the customer id
            Dictionary<int, int> customerId = new Dictionary<int, int>();

            //loop through the customer list and add them to the console
            foreach (Customer c in currentCustomers)
            {
                //add customer name to console
                Console.WriteLine(i + ". " + c.FirstName + " " + c.LastName);
                //add list number (key) and customer Id (value) to the dictionary
                customerId.Add(i, c.Id);
                //i is increased by 1 each time through
                i += 1;
            }
            Console.WriteLine(">");

            //parse the entered list item into an int, using ReadLine vs ReadKey since it is likely that more than 9 customers will be in the system at a given time
            int listItem = int.Parse(Console.ReadLine().ToString());

            if (listItem > 0)
            {
                if(!customerId.ContainsKey(listItem)){
                    //if the user enters a list item that does not exist then user will stay on the current screen
                    SelectCurrent(manager, db);
                } else{
                //gets the customer Id that matches the list item entered
                int activeId = customerId[listItem];
                //user selects a customer to set as active
                manager.SetActive(activeId);

                //update the active customer LastActive Date with today's date
                string activeDate = DateTime.Now.ToString();
                db.Update($@"UPDATE Customer
                    SET LastActive = '{activeDate}'
                    WHERE Id = {activeId};");

                //bring user to the Customer Menu
                CustomerMenu.DisplayMenu();
                }

            }
            else
            {
                //if user selects 0 return to the main menu
                return;
            }

        }
    }
}