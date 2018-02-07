
using System;
using System.Collections.Generic;
using Xunit;

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
            //create new customer using commandline
            Customer bob = new Customer();
            Assert.Equal(bob.FirstName, "Bob");
            Assert.Equal(bob.LastName, "Jones");
            Assert.Equal(bob.Address, "200 Jackson Lane");
            Assert.Equal(bob.City, "Nashville");
            Assert.Equal(bob.State, "TN");
            Assert.Equal(bob.PostalCode, "12345");
            Assert.Equal(bob.Phone, "123-123-1234");
        }

        [Fact]
        public void ListCustomers()
        {
            //adds customer to the list of customers
            _manager.Add(_joe);
            List<Customer> customerList = _manager.GetAllCustomers();

            Assert.Contains(_joe, customerList);

        }

        public void ActiveCustomer()
        {
            //passes in the Customer's Id to make that customer the active customer
            _manager.SetActive(_joe.Id);

            Assert.Equal(_manager.GetActive(), 1);

        }
    }
}