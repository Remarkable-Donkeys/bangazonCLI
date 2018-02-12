using System;
using System.Collections.Generic;
using System.Linq;

namespace bangazonCLI
{
    public class CloseOrderMenu
    {
		private static string DBenvironment = "BANGAZONCLI";
		private static int activeCustomerId = CustomerManager.ActiveCustomerId;
		private static OrderManager orderManager = new OrderManager(DBenvironment);
		private static Order targetOrder;
        public static void Show()
        {        
			/*
				Choose a payment option
				1. Amex
				2. Visa
				>
			*/ 
			ProductManager productManager = new ProductManager(DBenvironment);
			PaymentTypeManager paymentTypeManager = new PaymentTypeManager();

			int userChoice;
			int paymentTypeDictId = 0;

			Dictionary<int, PaymentType> paymentTypeDict = new Dictionary<int, PaymentType>();
			
			List<PaymentType> paymentTypeList = paymentTypeManager.GetPaymentTypesList(activeCustomerId);
			paymentTypeList.ForEach(paymentType => paymentTypeDict.Add(++paymentTypeDictId, paymentType));
			
			if(paymentTypeList.Count > 0)
			{
				do {
					Console.Clear();
					Console.WriteLine("Choose a Payment Option");
					Console.WriteLine("**********************");
					
					foreach(KeyValuePair<int, PaymentType> paymentTypeKvp in paymentTypeDict)
					{
						Console.WriteLine($"{paymentTypeKvp.Key}. {paymentTypeKvp.Value.Type}");
					}

					Console.Write("> ");
					userChoice = Int32.Parse(Console.ReadLine());

					if(paymentTypeDict.ContainsKey(userChoice))
					{
						orderManager.CompleteOrder(targetOrder.Id, paymentTypeDict[userChoice].Id);
					}
				} while(userChoice != paymentTypeDictId);
			}
			else
			{
				Console.WriteLine("Please register a valid Payment Type, press any key go go back to the previous menu");
				Console.ReadKey();				
			}
        }

        public static void DisplayMenu()
        {
			var activeOrderForActiveUser = orderManager.GetOrderList().Where(order => order.CustomerId == activeCustomerId && order.PaymentTypeId == null).FirstOrDefault();
			if(activeOrderForActiveUser != null)
			{
            	targetOrder = activeOrderForActiveUser;
				List<double> priceList = new List<double>();
				targetOrder.GetProductList().ForEach(product => priceList.Add(product.Price));
				double orderTotal = priceList.Sum();

				orderManager.GetOrderList().Where(order => order.Id == activeOrderForActiveUser.Id && order.PaymentTypeId == null).Single();
				Console.WriteLine($"Your order total is ${orderTotal}. Ready to purchase? (Y/N)");
				Console.WriteLine("> ");
				Char userChoice = Char.Parse(Console.ReadLine());
				if(userChoice == 'Y' || userChoice == 'y')
				{
					Show();
				}

			}
			else
			{
				Console.WriteLine("Please add some products to your order first. Press any key to return to main menu.");
				Console.ReadKey();				
            	ProductPurchaseMenu.DisplayMenu();
			}
			ProductPurchaseMenu.DisplayMenu();
        }
    }
}