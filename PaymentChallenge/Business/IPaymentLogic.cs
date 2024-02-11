using PaymentChallenge.Data;
using PaymentChallenge.Model;

namespace PaymentChallenge.Business
{
    public interface IPaymentLogic
    {
        ResultResponse NewPayment(Payment newPayment);
        ResultResponse ConfirmPayment(Payment paymentToConfirm);
    }
}