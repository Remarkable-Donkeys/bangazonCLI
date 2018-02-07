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

		public PaymentTypeManagerShould()
		{
			db = new DatabaseInterface();
			db.CheckDatabase();
			_payment = new PaymentType(1, "VISA", "1234567");
			_manager = new PaymentTypeManager();

		}
        [Fact]
        public void AddPaymentType()
        {
			db.Insert($@"
            INSERT INTO Customer
            (Id, FirstName, LastName, DateCreated)
            VALUES
            (null, 'Sean', 'Williams', '2018-01-01')
            ");
			List<PaymentType> paymentList = new List<PaymentType>();
			_manager.AddPaymentType(_payment);
			db.Query($@"
                SELECT P.Id, P.CustomerId, P.Type, P.AccountNumber FROM PaymentType P
				Where P.CustomerId = {_payment.CustomerId},
				where P.Type = '{_payment.Type}',
				where P.Number = '{_payment.AccountNumber}'
               
            ", (SqliteDataReader handler) =>
            {
                while (handler.Read())
                {
                    PaymentType payment = new PaymentType(
                        int.Parse(handler.GetString(1)),
                        handler.GetString(2),
                        handler.GetString(3)
                    );
                    payment.SetId(int.Parse(handler.GetString(0)));
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
			CustomerManager customerManager = new CustomerManager();
            customerManager.SetActive(1);
			_manager.AddPaymentType(_payment);
			List<PaymentType> paymentList = _manager.GetPaymentTypesList(customerManager.GetActive());
			Assert.Contains(_payment, paymentList);
		}
    }
}
