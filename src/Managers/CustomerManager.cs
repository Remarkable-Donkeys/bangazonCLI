using System;
using System.Collections.Generic;

namespace bangazonCLI
{
    public class CustomerManager
    {
        private static DatabaseInterface _db = new DatabaseInterface();
        private List<Customer> _customerList = new List<Customer>();

        public static int ActiveCustomerId { get; set; }

        public void Add(string first, string last, string address, string city, string state, string postalCode, string phone, DateTime dateCreated, DateTime lastActive)
        {
            //adds customer to the database of customers
            _db.Insert($@"
                INSERT INTO `Customer`
                (`Id`, `FirstName`, `LastName`, `DateCreated`, `LastActive`, `Address`, `City`, `State`, `PostalCode`, `Phone`)
                VALUES
                (null, '{first}', '{last}', '{dateCreated}', '{lastActive}', '{address}', '{city}', '{state}', '{postalCode}', '{phone}')
            ");
        }

        public List<Customer> GetAllCustomers()
        {
            //returns the list of customers
            return _customerList;
        }

        public void SetActive(int id)
        {
            //sets the customer as the active customer
            ActiveCustomerId = id;
        }

        public int GetActive()
        {
            //returns the id of the active customer
            return _activeCustomerId;
        }




    }
}