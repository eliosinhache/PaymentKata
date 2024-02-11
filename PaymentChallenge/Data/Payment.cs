using PaymentChallenge.Data.Enum;

namespace PaymentChallenge.Data
{
    public class Payment
    {
        public int ClientId { get; internal set; }

        public float Amount { get; internal set; }
        public DateTime DateEntry { get; internal set; }
        public int Id { get; internal set; }
        public OperationType OperationType { get; internal set; }
        public string PaymentCode { get; internal set; }
    }
}
