using System;
using System.Collections.Generic;
using System.Linq;

namespace bangazonCLI
{
    public class ProductListMenu
    {
		private static string DBenvironment = "BANGAZONCLI";
        public static void Show()
        {            
			OrderManager orderManager = new OrderManager(DBenvironment);
			ProductManager productManager = new ProductManager(DBenvironment);
			int goBackToProductPurchaseMenu;
			int userChoice;
			int activeCustomerId = CustomerManager.ActiveCustomerId;
			do {
				List<Product> productList = productManager.GetAllProducts();
				Dictionary<int, Product> productDict = new Dictionary<int, Product>();
				int productDictId = 0;
				Product selectedProduct;

				productList.ForEach(product => productDict.Add(++productDictId, product));
				Console.Clear();
				Console.WriteLine("Select Product to Purchase");
				Console.WriteLine("**********************");
				
				// productList.ForEach(product => Console.WriteLine($"{++productIndex}. {product.Name}"));
				foreach(KeyValuePair<int, Product> productKvp in productDict)
				{
					Console.WriteLine($"{productKvp.Key}. {productKvp.Value.Name}");
				}
				
				goBackToProductPurchaseMenu = productDictId + 1;

				Console.WriteLine($"{goBackToProductPurchaseMenu}. Done adding products");

				Console.Write("> ");
				userChoice = Int32.Parse(Console.ReadLine());

				// not accounting for if user selects to go back, throwing an error that id is not in dict
				// selectedProduct = productDict.Where(dict => kvp.Key == userChoice).Single();
				if(userChoice != goBackToProductPurchaseMenu)
				{
					selectedProduct = productDict[userChoice];
					// if there is an existing order
					// if(orderManager.GetOrderList().Where(order => order.CustomerId == CustomerManager.ActiveCustomerId && order.PaymentTypeId != null).Single())
					var isThereAnActiveOrderForActiveUser = orderManager.GetOrderList().Where(order => order.CustomerId == activeCustomerId && order.PaymentTypeId == null).FirstOrDefault(); 
					if(isThereAnActiveOrderForActiveUser == null)
					{
						// else create an order
						Order tempOrder = new Order(DBenvironment);
						tempOrder.CustomerId = CustomerManager.ActiveCustomerId;
						tempOrder.DateCreated = DateTime.Now;
						// add order to order manager
						orderManager.AddOrder(tempOrder);												
					}
					
					Order targetOrder = orderManager.GetOrderList().Where(order => order.CustomerId == activeCustomerId && order.PaymentTypeId == null).Single();


					// int targetOrderId = targetOrder.Id;
					targetOrder.AddProduct(selectedProduct);
				}
			} while(userChoice != goBackToProductPurchaseMenu);
        }

        public static void DisplayMenu()
        {
            Show();
            ProductPurchaseMenu.DisplayMenu();
        }
    }
}