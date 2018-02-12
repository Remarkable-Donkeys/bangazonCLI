using System;
using System.Collections.Generic;
using System.Linq;

namespace bangazonCLI
{
    public static class RevenueReportInterface
    {   
        public static void Display()
        {
            CustomerManager customerManager = new CustomerManager("BANGAZONCLI");
            List<Customer> allCustomers = customerManager.GetAllCustomers();
            Customer activeCustomer = allCustomers.Where(c=> c.Id == customerManager.GetActive()).Single();
            RevenueReportManager revManager = new RevenueReportManager();
            OrderManager orderManager = new OrderManager("BANGAZONCLI");
            List<Order> allOrders = orderManager.GetOrderList();
            List<Order> customerOrders = allOrders.Where(o => o.CustomerId == activeCustomer.Id).ToList();

            
           

            Console.WriteLine($"Revenue report for {activeCustomer.FirstName} {activeCustomer.LastName}:");
            Console.WriteLine();
            
            foreach(Order o in customerOrders)
            {
                Dictionary<string, (int, double)> orderRevenue = revManager.GetProductsDictionary(o);
            }

        }
    }
}