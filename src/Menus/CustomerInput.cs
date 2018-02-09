/*author:   Kristen Norris
purpose:    Allows user to create a new customer
method:    New: prompts user to enter information for the customer. Once the user has entered all the information, it adds the customer to the database, sets that customer as active and brings the user to the Customer Menu
*/
using System;

namespace bangazonCLI
{
    public class CustomerInput
    {
        public static void New(CustomerManager manager)
        {
            //user input to create a new customer
            Customer newCustomer = new Customer();
            Console.Clear();
            Console.WriteLine("Enter customer first name");
            Console.Write("> ");
            newCustomer.FirstName = Console.ReadLine();
            Console.WriteLine("Enter customer last name");
            Console.Write("> ");
            newCustomer.LastName = Console.ReadLine();
            Console.WriteLine("Enter customer address");
            Console.Write("> ");
            newCustomer.Address = Console.ReadLine();
            Console.WriteLine("Enter customer city");
            Console.Write("> ");
            newCustomer.City = Console.ReadLine();
            Console.WriteLine("Enter customer state");
            Console.Write("> ");
            newCustomer.State = Console.ReadLine();
            Console.WriteLine("Enter customer postal code");
            Console.Write("> ");
            newCustomer.PostalCode = Console.ReadLine();
            Console.WriteLine("Enter customer phone number");
            Console.Write("> ");
            newCustomer.Phone = Console.ReadLine();
            //when you create a new customer the DateCreated and the LastActive is set as the current Date/Time
            newCustomer.DateCreated = DateTime.Now;
            newCustomer.LastActive = DateTime.Now;

            //add a new customer to the database and return the id of the newly added customer
            int activeId = manager.Add(newCustomer);
            //set that customer as active
            manager.SetActive(activeId);
            //bring user to the Customer Menu
            CustomerMenu.DisplayMenu();
        }
    }
}