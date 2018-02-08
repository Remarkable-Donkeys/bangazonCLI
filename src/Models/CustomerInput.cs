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
            Console.Clear();
            Console.WriteLine("Enter customer first name");
            Console.Write("> ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter customer last name");
            Console.Write("> ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter customer address");
            Console.Write("> ");
            string address = Console.ReadLine();
            Console.WriteLine("Enter customer city");
            Console.Write("> ");
            string city = Console.ReadLine();
            Console.WriteLine("Enter customer state");
            Console.Write("> ");
            string state = Console.ReadLine();
            Console.WriteLine("Enter customer postal code");
            Console.Write("> ");
            string postalCode = Console.ReadLine();
            Console.WriteLine("Enter customer phone number");
            Console.Write("> ");
            string phoneNumber = Console.ReadLine();
            //when you create a new customer the DateCreated and the LastActive is set as the current Date/Time
            DateTime dateCreated = DateTime.Now;
            DateTime lastActive = DateTime.Now;

            //take user input to add a new customer to the database and return the id of the newly added customer
            int activeId = manager.Add(firstName, lastName, address, city, state, postalCode, phoneNumber, dateCreated, lastActive);
            //set that customer as active
            manager.SetActive(activeId);
            //bring user to the Customer Menu
            CustomerMenu.DisplayMenu();
        }
    }
}