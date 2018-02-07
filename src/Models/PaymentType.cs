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

        public void SetId(int id)
        {
            Id = id;
        }
    }
}