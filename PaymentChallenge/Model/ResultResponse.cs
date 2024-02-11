using PaymentChallenge.Model.Response;

namespace PaymentChallenge.Model
{
    public class ResultResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public PaymentResponse Payment;
    }
}
