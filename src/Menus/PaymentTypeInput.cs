/*author:   Kristen Norris
purpose:    Allows user to create a new payment type for the active customer
method:    New: prompts user to enter information for the payment type.
*/
using System;

namespace bangazonCLI
{
    public class PaymentTypeInput
    {
        public static void New(PaymentTypeManager manager)
        {
            //user input to create a new payment type for the active customer
            Console.Clear();
            Console.WriteLine("Enter payment type (e.g. AmEx, Visa, Checking)");
            Console.Write("> ");
            string type = Console.ReadLine();
            Console.WriteLine("Enter account number");
            Console.Write("> ");
            string number = Console.ReadLine();
            //gets the id of the active customer
            int customerId = CustomerManager.ActiveCustomerId;

            //take user input to add a new customer to the database and return the id of the newly added customer
            PaymentType newPayment = new PaymentType(customerId, type, number);
            manager.AddPaymentType(newPayment);
            //bring user to the Customer Menu
            CustomerMenu.DisplayMenu();
        }
    }
}