namespace bangazonCLI
{
    public class PaymentType
    {
        public int Id;

        public int CustomerId;

        public string Type;

        public string Number;


        public PaymentType(int id, string type, string number) 
        {
            this.CustomerId = id;
            this.Type = type;
            this.Number = number;
        }
    }
}