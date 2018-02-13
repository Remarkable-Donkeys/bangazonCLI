using System;
using System.Collections.Generic;
namespace bangazonCLI
{
    public static class popularProductsInteface
    {
        public static string Display()
        {
            RevenueReportManager revManager = new RevenueReportManager();
            OrderManager orderManager = new OrderManager("BANGAZONCLI");
            ProductManager productManager = new ProductManager("BANGAZONCLI");

            List<Order> allOrders = orderManager.GetOrderList();
            List<Order> completedOrders = new List<Order>();

            foreach(Order o in allOrders)
            {
                if(o.PaymentTypeId != null)
                {
                    completedOrders.Add(o);
                }
            }

            Dictionary<string, (int, int, double)> popularProducts = revManager.GetPopularItems(completedOrders);

            int i = 1;


            Console.WriteLine("Product             Orders     Purchasers     Revenue        ");
            Console.WriteLine(new string('*', 61));
            foreach (KeyValuePair<string, (int, int, double)> p in popularProducts)
            {
                string output;
                if (i < 4)
                {
                    Product product = productManager.GetSingleProduct(Int32.Parse(p.Key));
                    string name = product.Name;

                    if (name.Length <= 18)
                    {
                        output = string.Format("{0, -20} {1, -11} {2, -15} {3, -15}", name, p.Value.Item1, p.Value.Item2, "$" + p.Value.Item3);

                    }
                    else
                    {
                        string shortName = name.Substring(0, 16);
                        shortName += "... ";
                        output = string.Format("{0, -20} {1, -11} {2, -15} {3, -15}", shortName, p.Value.Item1, p.Value.Item2, "$" + p.Value.Item3);
                    }
                    Console.WriteLine(output);
                }
                else
                {
                    Console.WriteLine(new string('*', 61));
                    output = string.Format("{0, -20} {1, -11} {2, -15} {3, -15}", p.Key, p.Value.Item1, p.Value.Item2, "$" + p.Value.Item3);
                    Console.WriteLine(output);
                }
                i++;
            }
            Console.WriteLine();
            Console.WriteLine("-> Press any key to return to main menu");
            
            return Console.ReadKey().ToString();;
           

        }
    }
}