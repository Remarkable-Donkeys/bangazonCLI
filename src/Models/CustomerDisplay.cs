using System;
using System.Collections.Generic;

namespace bangazonCLI
{
    public class CustomerDisplay
    {
        public CustomerDisplay(CustomerManager manager)
        {
            Console.Clear();
            Console.WriteLine("To return to main menu enter 'exit'");
            Console.WriteLine("Please enter Customer Id of the active customer");
            List<Customer> currentCustomers = manager.GetAllCustomers();
            foreach(Customer c in currentCustomers){
                int i = 1;
                Console.WriteLine(i + ". Name: " + c.FirstName + " " + c.LastName + " Phone: " + c.Phone);
            }
            Console.WriteLine(">");
            Console.ReadLine();
            
        }
    }
}