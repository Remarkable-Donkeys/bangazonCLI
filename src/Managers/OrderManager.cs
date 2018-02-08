using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
	public class OrderManager
	{
		/*******************/
		/* Class Variables */
		/*******************/
		private static DatabaseInterface _db = new DatabaseInterface();
		private List<Order> _orderList = new List<Order>();

		/*****************/
		/* Class Methods */
		/*****************/
		public int AddOrder(Order order)
		{
			return _db.Insert($@"
				INSERT INTO `Order`
				(`Id`, `CustomerId`, `DateCreated`)
				VALUES
				(null, {order.CustomerId}, '{order.DateCreated}')
			");
			// _orderList.Add(order);
		}

		public List<Order> GetOrderList()
		{
			//selects customer information from the database and adds it to a List<Customer>
            _db.Query($@"SELECT `Id`, `CustomerId`, `DateCreated`, `PaymentTypeId`, `DateOrdered` FROM `Order`",
            (SqliteDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();
                            
							order.Id = reader.GetInt32(0);
							order.CustomerId = reader.GetInt32(1);
                            order.DateCreated = reader.GetDateTime(2);
                            order.PaymentTypeId = reader[3] == System.DBNull.Value ? null : (int?)reader[3];
                            order.DateOrdered = reader[4] == System.DBNull.Value ? null : (DateTime?)reader.GetDateTime(4);

                            _orderList.Add(order);
                        }
                    });
            
            //returns the list of orders
			return _orderList;
		}

		public Boolean IsOrderInDatabase(int orderId)
		{
			// return _orderList.Contains(order);
			Order order = new Order();
			// return _orderList.Where(o => o.Id == orderId).Single();
			//selects customer information from the database and adds it to a List<Customer>
            _db.Query($@"SELECT `Id`, `CustomerId`, `DateCreated`, `PaymentTypeId`, `DateOrdered` FROM `Order` WHERE Id={orderId}",
				(SqliteDataReader reader) =>
				{
					while (reader.Read())
					{   
						order.Id = reader.GetInt32(0);
						order.CustomerId = reader.GetInt32(1);
						order.DateCreated = reader.GetDateTime(2);
						order.PaymentTypeId = reader[3] == System.DBNull.Value ? null : (int?)reader[3];
						order.DateOrdered = reader[4] == System.DBNull.Value ? null : (DateTime?)reader.GetDateTime(4);
					}
				});
            
            //returns the list of orders
			return order.Id == orderId;
		}

		public void RemoveAllOrders()
		{
			// _orderList.Clear();
			_db.Update($@"DELETE FROM `ORDER`");
		}

		public Order GetSingleOrder(int orderId)
		{
			Order order = new Order();
			// return _orderList.Where(o => o.Id == orderId).Single();
			//selects customer information from the database and adds it to a List<Customer>
            _db.Query($@"SELECT `Id`, `CustomerId`, `DateCreated`, `PaymentTypeId`, `DateOrdered` FROM `Order` WHERE Id={orderId}",
				(SqliteDataReader reader) =>
				{
					while (reader.Read())
					{   
						order.Id = reader.GetInt32(0);
						order.CustomerId = reader.GetInt32(1);
						order.DateCreated = reader.GetDateTime(2);
						order.PaymentTypeId = reader[3] == System.DBNull.Value ? null : (int?)reader[3];
						order.DateOrdered = reader[4] == System.DBNull.Value ? null : (DateTime?)reader.GetDateTime(4);
					}
				});
            
            //returns the list of orders
			return order;
		}

		public void RemoveOrder(int orderId)
		{
			// _orderList.RemoveAll(o => o.Id == orderId);
			_db.Update($@"DELETE FROM `Order` WHERE Id={orderId}");
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