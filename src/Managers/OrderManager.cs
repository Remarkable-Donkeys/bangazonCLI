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
	}
}