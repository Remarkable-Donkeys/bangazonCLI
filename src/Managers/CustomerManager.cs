using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
    public class CustomerManager
    {
        private static DatabaseInterface _db = new DatabaseInterface();
        private List<Customer> _customerList = new List<Customer>();

        public static int ActiveCustomerId { get; set; }


        public int Add(string first, string last, string address, string city, string state, string postalCode, string phone, DateTime dateCreated, DateTime lastActive)
        {
            //adds customer to the database of customers and returns the id of that customer
            return _db.Insert($@"
                INSERT INTO `Customer`
                (`Id`, `FirstName`, `LastName`, `DateCreated`, `LastActive`, `Address`, `City`, `State`, `PostalCode`, `Phone`)
                VALUES
                (null, '{first}', '{last}', '{dateCreated}', '{lastActive}', '{address}', '{city}', '{state}', '{postalCode}', '{phone}')
            ");
        }

        public int Add(Customer c)
        {
            //adds customer to the database of customers and returns the id of that customer
            return _db.Insert($@"
                INSERT INTO `Customer`
                (`Id`, `FirstName`, `LastName`, `DateCreated`, `LastActive`, `Address`, `City`, `State`, `PostalCode`, `Phone`)
                VALUES
                (null, '{c.FirstName}', '{c.LastName}', '{c.DateCreated}', '{c.LastActive}', '{c.Address}', '{c.City}', '{c.State}', '{c.PostalCode}', '{c.Phone}')
            ");
        }

        public List<Customer> GetAllCustomers()
        {
            //selects customer information from the database and adds it to a List<Customer>
            _db.Query($@"SELECT `Id`, `FirstName`, `LastName`, `DateCreated`, `LastActive`, `Address`, `City`, `State`, `PostalCode`, `Phone` FROM Customer",
            (SqliteDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.Id = reader.GetInt32(0);
                            customer.FirstName = reader[1].ToString();
                            customer.LastName = reader[2].ToString();
                            customer.DateCreated = reader.GetDateTime(3);
                            customer.LastActive = reader.GetDateTime(4);
                            customer.Address = reader[5].ToString();
                            customer.City = reader[6].ToString();
                            customer.State = reader[7].ToString();
                            customer.PostalCode = reader[8].ToString();
                            customer.Phone = reader[9].ToString();

                            _customerList.Add(customer);
                        }
                    });
            
            //returns the list of customers
            return _customerList;
        }

        public void SetActive(int id)
        {
            //sets the customer as the active customer
            ActiveCustomerId = id;
        }

        public int GetActive(){
            //returns the id of the active customer

            return ActiveCustomerId;
        }


    }
}