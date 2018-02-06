using System.Collections.Generic;

namespace bangazonCLI
{
    public class CustomerManager
    {
        private List<Customer> _customerList = new List<Customer>();

        private int _activeCustomerId {get; set;}

        public void Add(Customer c){
            //adds customer to the list of customers
            _customerList.Add(c);
        }

        public List<Customer> GetAllCustomers(){
            //returns the list of customers
            return _customerList;
        }

        public void SetActive(int id){
            //sets the customer as the active customer
            this._activeCustomerId = id;
        }

        public int GetActive(){
            //returns the id of the active customer
            return _activeCustomerId;
        }


        
        
    }
}