/*author:   Kristen Norris
purpose:    Customer Unit Tests
Tests:    	AddNewCustomer
			ActiveCustomer
 */
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
            //create a new customer
            Customer bob = new Customer(){
                FirstName = "Bob",
                LastName = "Jones",
                Address = "200 Jackson Lane",
                City= "Nashville",
                State= "TN",
                PostalCode= "12345",
                Phone="123-123-1234",
                DateCreated= DateTime.Now,
                LastActive=DateTime.Now
            };
            _manager.Add(bob);
            //gets list of all customers
            List<Customer> customerList = _manager.GetAllCustomers();

            bool bobExists = false;
            //if bob is in the list then set bobExists to true
            foreach(Customer c in customerList){
                if(c.FirstName == "Bob" && c.LastName == "Jones" && c.Address == "200 Jackson Lane" && c.Phone=="123-123-1234"){
                    bobExists = true;
                }
            }
            Assert.True(bobExists);
        }

        public void ActiveCustomer()
        {
            //passes in the Customer's Id to make that customer the active customer
            _manager.SetActive(2);


            Assert.Equal(2, CustomerManager.ActiveCustomerId);

        }


    }
}