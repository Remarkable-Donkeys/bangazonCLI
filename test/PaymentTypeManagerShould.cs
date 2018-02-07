using System;
using System.Collections.Generic;
using Xunit;
using bangazonCLI;

namespace bangazonCLI.Test
{
    public class PaymentTypeManagerShould
    {

		PaymentType _payment;
		PaymentTypeManager _manager;

		public PaymentTypeManagerShould()
		{
			_payment = new PaymentType(1, "VISA", "1234567");
			_manager = new PaymentTypeManager();
		}
        [Fact]
        public void AddPaymentType()
        {

			Assert.Equal("VISA", _payment.Type);
			Assert.Equal("1234567", _payment.Number);
			Assert.Equal(1, _payment.CustomerId);
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
