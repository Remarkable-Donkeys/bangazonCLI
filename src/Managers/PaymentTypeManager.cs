using System.Collections.Generic;
using System.Linq;

namespace bangazonCLI
{
    public class PaymentTypeManager
    {
        private List<PaymentType> _paymentList = new List<PaymentType>();
        public void AddPaymentType(PaymentType payment)
        {
            _paymentList.Add(payment);
        }

        public List<PaymentType> GetPaymentTypesList(int customer)
        {
            return _paymentList.Where(p => p.CustomerId == customer).ToList();
        }
    }
}