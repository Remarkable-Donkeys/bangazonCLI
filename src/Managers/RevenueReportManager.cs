using System;
using System.Linq;
using System.Collections.Generic;

namespace bangazonCLI
{
    public class RevenueReportManager
    {
        private DatabaseInterface db;

        public RevenueReportManager(string dbInterface)
        {
            db = new DatabaseInterface(dbInterface);
        } 
        public RevenueReportManager()
        {
            db = new DatabaseInterface("BANGAZONCLI");
        }

        public Dictionary<string,double> GetProductsDictionary(Order input)
        {
            Dictionary<string, double> resDictionary = new Dictionary<string,double>();
            List<Product> productList = input.GetProductList();
            double total = 0;

            foreach (Product item in productList)
            {
                if (resDictionary.ContainsKey(item.Id.ToString()))
                {
                    resDictionary[item.Id.ToString()] += item.Price;
                    total += item.Price;
                } 
                else
                {
                    resDictionary.Add(item.Id.ToString(), item.Price);  
                    total += item.Price;
                }
            }

            resDictionary.Add("Total", total);

            return resDictionary;
        }
    }
}