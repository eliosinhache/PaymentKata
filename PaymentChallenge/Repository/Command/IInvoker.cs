namespace PaymentChallenge.Repository.Command
{
    public interface IInvoker
    {
        void AddCommand(ICommand implementation);
        void ExecuteCommands();
        bool IsTransactionComplete();
    }
}