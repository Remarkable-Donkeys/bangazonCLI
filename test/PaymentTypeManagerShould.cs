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

		private PaymentType _payment;
		private PaymentTypeManager _manager;
		private DatabaseInterface _db;

		private Customer _testCustomer;

		private CustomerManager _customerManager;

		public PaymentTypeManagerShould()
		{
			_db = new DatabaseInterface("BANGAZONTEST");
			_payment = new PaymentType(1, "VISA", "1234567");
			_manager = new PaymentTypeManager("BANGAZONTEST");
			_testCustomer = new Customer("Sean", "Williams");
			_customerManager = new CustomerManager("BANGAZONTEST");

		}
        [Fact]
        public void AddPaymentType()
        {
			_db.NukeDB();
			_db.CheckDatabase();
			_payment.CustomerId=_customerManager.Add(_testCustomer);
			List<PaymentType> paymentList = new List<PaymentType>();
			_manager.AddPaymentType(_payment);
			_db.Query($@"
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
			_db.NukeDB();
			Assert.Equal(1, paymentList.Count);
			Assert.Equal("VISA", paymentList[0].Type);
			Assert.Equal("1234567", paymentList[0].AccountNumber);
			Assert.Equal(_payment.CustomerId, paymentList[0].CustomerId);
        }

		// // [Fact]
		// // public void GetPaymentTypesList()
		// // {
		// // 	_db.NukeDB();
		// // 	_db.CheckDatabase();
		// // 	_payment.CustomerId= _customerManager.Add(_testCustomer);
        // //     _customerManager.SetActive(1);
		// // 	_manager.AddPaymentType(_payment);
		// // 	List<PaymentType> paymentList = _manager.GetPaymentTypesList(_customerManager.GetActive());
		// // 	_db.NukeDB();
		// // 	Assert.Contains(_payment, paymentList);
		// }
    }
}
