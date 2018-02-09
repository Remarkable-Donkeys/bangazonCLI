/*author:   Sean Williams
purpose:    Handle database interactions pertaining to Payment Types
methods:    AddPaymentType
            GetPaymentTypeList
 */
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
    public class PaymentTypeManager
    {
        //Stores ALL payment types
        private List<PaymentType> _paymentList;
        private DatabaseInterface db;
        public PaymentTypeManager(string dbInterface)
        {
            _paymentList = new List<PaymentType>();
            db = new DatabaseInterface(dbInterface);

            //This query gets all payment type information from the database
            // and pushes the result into a list of payment types
            db.Query($@"
                SELECT P.Id, P.CustomerId, P.Type, P.AccountNumber FROM PaymentType P
               
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
                    _paymentList.Add(payment);
                }
            });
        } 
        public PaymentTypeManager()
        {
            _paymentList = new List<PaymentType>();
            db = new DatabaseInterface("BANGAZONCLI");

            //This query gets all payment type information from the database
            // and pushes the result into a list of payment types
            db.Query($@"
                SELECT P.Id, P.CustomerId, P.Type, P.AccountNumber FROM PaymentType P
               
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
                    _paymentList.Add(payment);
                }
            });
        }

        //Adds a payment type to the database
        public int AddPaymentType(PaymentType payment)
        {
            _paymentList.Add(payment);
		    return db.Insert($@"
            INSERT INTO PaymentType
            (Id, CustomerId, Type, AccountNumber)
            VALUES
            (null, {payment.CustomerId}, '{payment.Type}', '{payment.AccountNumber}')
            ");
        }

        //return a list of all product types conected to the CustomerId passed in
        public List<PaymentType> GetPaymentTypesList(int customer)
        {
            return _paymentList.Where(p => p.CustomerId == customer).ToList();
        }
    }
}