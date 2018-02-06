using System;

namespace bangazonCLI
{
    public class Customer
    {
        public int CustomerId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Address {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string PostalCode {get; set;}
        public string Phone {get; set;}

    
        public Customer(string first, string last){
            this.CustomerId = 1;
            this.FirstName = first;
            this.LastName = last;
        }

        public Customer()
        {
            Console.Clear();
            Console.WriteLine("Enter customer first name");
            Console.Write("> ");
            this.FirstName = Console.ReadLine();
            Console.WriteLine("Enter customer last name");
            Console.Write("> ");
            this.LastName = Console.ReadLine();
            Console.WriteLine("Enter customer address");
            Console.Write("> ");
            this.Address= Console.ReadLine();
            Console.WriteLine("Enter customer city");
            Console.Write("> ");
            this.City= Console.ReadLine();
            Console.WriteLine("Enter customer state");
            Console.Write("> ");
            this.State= Console.ReadLine();
            Console.WriteLine("Enter customer postal code");
            Console.Write("> ");
            this.PostalCode= Console.ReadLine();
            Console.WriteLine("Enter customer phone number");
            Console.Write("> ");
            this.Phone = Console.ReadLine();
        }


    }
}