using PaymentChallenge.Data;

namespace PaymentChallenge.Contract.DB
{
    //public interface IPaymentRepository : IGenericRepository<Payment>
    public interface IPaymentRepository 
    {
        Payment GetApproved(string paymentCode);
        bool Exists(string paymentCode, float amount);
        bool ApprovedPaymentExists(string paymentCode, float amount);
    }
}
