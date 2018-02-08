/*author:   Sean Williams
purpose:    Payment Types model
methods:    NULL
 */
 
 namespace bangazonCLI
{
    public class PaymentType
    {
        public int Id;

        public int CustomerId;

        public string Type;

        public string AccountNumber;


        public PaymentType(int customer, string type, string number) 
        {
            this.CustomerId = customer;
            this.Type = type;
            this.AccountNumber = number;
        }
    }
}