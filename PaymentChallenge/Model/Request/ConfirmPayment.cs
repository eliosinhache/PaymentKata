using PaymentChallenge.Data.Enum;

namespace PaymentChallenge.Model.Request
{
    public class ConfirmPayment : BasePaymentModel
    {
        public string PaymentCode { get; set; }
        public OperationType OperationType { get; set; }

    }
}
