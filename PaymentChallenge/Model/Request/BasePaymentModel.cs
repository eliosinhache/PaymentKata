namespace PaymentChallenge.Model.Request
{
    public abstract class BasePaymentModel
    {
        public int ClientID { get; set; }
        public float Amount { get; set; }
    }
}
