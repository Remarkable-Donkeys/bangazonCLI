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
            
            double grandTotal = 0.00;

            foreach(Order o in customerOrders)
            {
                Dictionary<string, (int, double)> orderRevenue = revManager.GetProductsDictionary(o);
                grandTotal += orderRevenue["Total"].Item2;
                Console.WriteLine($"Order #{o.Id}");
                Console.WriteLine(new string('-', 55));
                string output = string.Format("{0, -20} {1, -15} {2, -15}", "Total", orderRevenue["Total"].Item1, orderRevenue["Total"].Item2);
                Console.WriteLine(output);

            }

            Console.WriteLine($"Total Revenue: ${grandTotal}");

        }
    }
}