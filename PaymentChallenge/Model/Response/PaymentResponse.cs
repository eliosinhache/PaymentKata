using PaymentChallenge.Data.Enum;
using PaymentChallenge.Model.Request;

namespace PaymentChallenge.Model.Response
{
    public class PaymentResponse : BasePaymentModel
    {
        public string PaymentCode { get; set; }
        public OperationType OperationType { get; set; }
    }
}
