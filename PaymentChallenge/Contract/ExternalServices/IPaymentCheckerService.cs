namespace PaymentChallenge.Contract.ExternalServices
{
    public interface IPaymentCheckerService
    {
        Task<bool> IsPaymentValid(float amount);
    }
}
