
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Data.Sqlite;


namespace bangazonCLI.Tests
{
    public class CustomerManagerShould
    {
        //creates new customer
        private Customer _joe = new Customer("Joe", "Smith");
        //new instance of customer manager
        private CustomerManager _manager = new CustomerManager();

        [Fact]
        public void AddNewCustomer()
        {
            DatabaseInterface db = new DatabaseInterface();
            //create new customer using commandline
            // _manager.Add(_joe);
            List<Customer> customerList = _manager.GetAllCustomers();

            bool bobExists = false;
            foreach(Customer c in customerList){
                if(c.FirstName == "Bob" && c.LastName == "Jones" && c.Address == "200 Jackson Lane"){
                    bobExists = true;
                }
            }
            Assert.True(bobExists);

        }

        // [Fact]
        // public void ListCustomers()
        // {
        //     //adds customer to the list of customers
        //     _manager.Add(_joe);
        //     List<Customer> customerList = _manager.GetAllCustomers();

        //     Assert.Contains(_joe, customerList);

        // }

        // public void ActiveCustomer()
        // {
        //     //passes in the Customer's Id to make that customer the active customer
        //     _manager.SetActive(_joe.Id);

        //     Assert.Equal(_manager.GetActive(), 1);

        // }

    }
}