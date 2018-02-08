/*author:   Sean Williams
purpose:    Payment Types Unit Tests
Tests:    	AddPaymentType
			GetPaymentTypesList
 */
 
 using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Xunit;
using bangazonCLI;

namespace bangazonCLI.Test
{
    public class PaymentTypeManagerShould
    {

		PaymentType _payment;
		PaymentTypeManager _manager;
		DatabaseInterface db;

		Customer testCustomer;

		CustomerManager customerManager;

		public PaymentTypeManagerShould()
		{
			db = new DatabaseInterface();
			_payment = new PaymentType(1, "VISA", "1234567");
			_manager = new PaymentTypeManager();
			testCustomer = new Customer("Sean", "Williams");
			customerManager = new CustomerManager();

		}
        [Fact]
        public void AddPaymentType()
        {
			db.NukeDB();
			db.CheckDatabase();
			Customer testCustomer = new Customer("Sean", "Williams");
			CustomerManager customerManager = new CustomerManager();
			customerManager.Add(testCustomer);
			List<PaymentType> paymentList = new List<PaymentType>();
			_manager.AddPaymentType(_payment);
			db.Query($@"
                SELECT P.Id, P.CustomerId, P.Type, P.AccountNumber FROM PaymentType P
				Where P.CustomerId = {_payment.CustomerId}
				and P.Type = '{_payment.Type}'
				and P.AccountNumber = '{_payment.AccountNumber}'
               
            ", (SqliteDataReader handler) =>
            {
                while (handler.Read())
                {
                    PaymentType payment = new PaymentType(
                        int.Parse(handler.GetString(1)),
                        handler.GetString(2),
                        handler.GetString(3)
                    );
                    payment.Id = int.Parse(handler.GetString(0));
					paymentList.Add(payment);
                }
            });
			Assert.Equal(1, paymentList.Count);
			Assert.Equal("VISA", paymentList[0].Type);
			Assert.Equal("1234567", paymentList[0].AccountNumber);
			Assert.Equal(1, paymentList[0].CustomerId);
        }

		[Fact]
		public void GetPaymentTypesList()
		{
			db.NukeDB();
			db.CheckDatabase();
			customerManager.Add(testCustomer);
            customerManager.SetActive(1);
			_manager.AddPaymentType(_payment);
			List<PaymentType> paymentList = _manager.GetPaymentTypesList(customerManager.GetActive());
			Assert.Contains(_payment, paymentList);
		}
    }
}
