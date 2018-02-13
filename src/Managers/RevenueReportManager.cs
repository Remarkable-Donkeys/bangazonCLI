/*author:   Sean Williams
purpose:    Handles the logic for finding popular products and a customer's revenue report
methods:    GetProductsDictionary
            GetPopularItems
 */
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

		// Consumes an order and returns a dictionary with the product Id, the total units of each
		// product, and the total revenue of each product.  Also contains total items sold and total
		// revenue for the order under key "Total"
		public Dictionary<string, (int, double)> GetProductsDictionary(Order input)
		{
			Dictionary<string, (int, double)> resDictionary = new Dictionary<string, (int, double)>();
			Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();
			Dictionary<string, double> itemPriceDictionary = new Dictionary<string, double>();
			List<Product> productList = input.GetProductList();
			double total = 0;
			int allProducts = 0;

			foreach (Product item in productList)
			{
				if (itemCountDictionary.ContainsKey(item.Id.ToString()))
				{
					itemCountDictionary[item.Id.ToString()] += 1;
					itemPriceDictionary[item.Id.ToString()] += item.Price;
					total += item.Price;
					allProducts += 1;
				}
				else
				{
					itemCountDictionary.Add(item.Id.ToString(), 1);
					itemPriceDictionary.Add(item.Id.ToString(), item.Price);
					total += item.Price;
					allProducts += 1;
				}
			}

			foreach (KeyValuePair<string, int> product in itemCountDictionary)
			{
				resDictionary.Add(product.Key, (itemCountDictionary[product.Key], itemPriceDictionary[product.Key]));
			}

			resDictionary.Add("Total", (allProducts, total));

			return resDictionary;
		}

		// Consumes an order and returns a dictionary with the product Id, the total units of each
		// product, and the total revenue of each product.  Also contains total items sold and total
		// revenue for the order under key "Total".  ONLY CONTAINS PRODUCTS ASSIGNED TO CUSTID PARAM
		public Dictionary<string, (int, double)> GetProductsDictionary(Order input, int custId)
		{
			Dictionary<string, (int, double)> resDictionary = new Dictionary<string, (int, double)>();
			Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();
			Dictionary<string, double> itemPriceDictionary = new Dictionary<string, double>();
			List<Product> productList = input.GetProductList();
			double total = 0;
			int allProducts = 0;

			foreach (Product item in productList)
			{
				if (item.CustomerId != custId)
				{
					continue;
				}
				if (itemCountDictionary.ContainsKey(item.Id.ToString()))
				{
					itemCountDictionary[item.Id.ToString()] += 1;
					itemPriceDictionary[item.Id.ToString()] += item.Price;
					total += item.Price;
					allProducts += 1;
				}
				else
				{
					itemCountDictionary.Add(item.Id.ToString(), 1);
					itemPriceDictionary.Add(item.Id.ToString(), item.Price);
					total += item.Price;
					allProducts += 1;
				}
			}

			foreach (KeyValuePair<string, int> product in itemCountDictionary)
			{
				resDictionary.Add(product.Key, (itemCountDictionary[product.Key], itemPriceDictionary[product.Key]));
			}

			resDictionary.Add("Total", (allProducts, total));

			return resDictionary;
		}

		// Consumes a list of orders and outputs a dictionary that contains the id of the top
		// three products sold (sorted by revenue), the number of orders containing said products,
		// the number of unique customers who orders those products, and the total revenue of those
		// products.  Totals of those fields are also included under the key "Total"
		public Dictionary<string, (int, int, double)> GetPopularItems(List<Order> orders)
		{
			Dictionary<string, (int, int, double)> res = new Dictionary<string, (int, int, double)>();
			Dictionary<string, int> itemOrders = new Dictionary<string, int>();
			Dictionary<string, double> itemRevenue = new Dictionary<string, double>();
			int allOrders = 0;
			int allPurchasers = 0;
			double totalRevenue = 0;
			Dictionary<string, (int, double)> productDictionay = new Dictionary<string, (int, double)>();

			Dictionary<string, List<int>> itemPurchasers = new Dictionary<string, List<int>>();

			foreach (Order or in orders)
			{
				productDictionay = this.GetProductsDictionary(or);
                productDictionay.Remove("Total");

				foreach (KeyValuePair<string, (int, double)> product in productDictionay)
				{
					if (itemRevenue.ContainsKey(product.Key))
					{
						itemRevenue[product.Key] += product.Value.Item2;
						itemOrders[product.Key] += 1;
						if (!itemPurchasers[product.Key].Contains(or.CustomerId))
						{
							itemPurchasers[product.Key].Add(or.CustomerId);
						}
					}
					else
					{
						itemRevenue.Add(product.Key, product.Value.Item2);
						itemOrders.Add(product.Key, 1);
						itemPurchasers.Add(product.Key, new List<int>());
						itemPurchasers[product.Key].Add(or.CustomerId);
					}
				}
			}

            var topProducts = itemRevenue.OrderByDescending(x => x.Value).Take(3);


			foreach (KeyValuePair<string, double> product in topProducts)
			{
				res.Add(product.Key, (itemOrders[product.Key], itemPurchasers[product.Key].Count, itemRevenue[product.Key]));
                allOrders += itemOrders[product.Key];
                allPurchasers += itemPurchasers[product.Key].Count;
                totalRevenue += itemRevenue[product.Key];

			}

			res.Add("Total", (allOrders, allPurchasers, totalRevenue));

			return res;
		}
	}
}