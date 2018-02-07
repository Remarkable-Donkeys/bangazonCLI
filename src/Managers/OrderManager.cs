using System;
using System.Linq;
using System.Collections.Generic;

namespace bangazonCLI
{
	public class OrderManager
	{
		/*******************/
		/* Class Variables */
		/*******************/
		private List<Order> _orderList = new List<Order>();

		/*****************/
		/* Class Methods */
		/*****************/
		public void AddOrder(Order order)
		{
			_orderList.Add(order);
		}
		public List<Order> GetOrderList()
		{
			return _orderList;
		}
		public Boolean IsOrderInOrderManager(Order order)
		{
			return _orderList.Contains(order);
		}
		public void RemoveAllOrders()
		{
			_orderList.Clear();
		}
		public Order GetSingleOrder(int orderId)
		{
			return _orderList.Where(o => o.Id == orderId).Single();
		}
		public void RemoveOrder(int orderId)
		{
			_orderList.RemoveAll(o => o.Id == orderId);
		}
		public void CompleteOrder(int orderId, int paymentId)
		{
            // order should have at least one product
			if(_orderList.Count < 1)
			{
				Console.WriteLine("Cannot complete an order that doesn't have any products");
				return;
			}

			// get copy of target order
			Order order = _orderList.Where(o => o.Id == orderId).Single();		

            // to complete order capture DateTime.Now
			order.PaymentTypeId = paymentId;

            // remove target order from orderManager
			_orderList.RemoveAll(o => o.Id == orderId);

			// assign a Date completed to order
			order.DateOrdered = DateTime.Now;

            // insert the new, modified version of the target order(the copy)
			_orderList.Add(order);
		}
	}
}