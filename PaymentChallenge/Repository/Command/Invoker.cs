using System.Data.SqlClient;

namespace PaymentChallenge.Repository.Command
{
    public class Invoker : IInvoker
    {
        private SqlCommand _cmd = new SqlCommand();
        private List<ICommand> _commands = new List<ICommand>();
        private bool _isTransactionComplete = false;

        public Invoker(IConfiguration config)
        {
            _cmd.Connection = new SqlConnection(config.GetConnectionString("PaymentDB"));
        }

        public void AddCommand(ICommand implementation) { _commands.Add(implementation); }
        public void ExecuteCommands()
        {
            _cmd.Connection.Open();
            using (var trans = _cmd.Connection.BeginTransaction())
            {
                _cmd.Transaction = trans;
                _cmd.CommandTimeout = 10;
                try
                {
                    foreach (ICommand command in _commands)
                    {
                        command.execute(_cmd);
                    }
                    trans.Commit();
                    _isTransactionComplete = true;
                }
                catch (System.Exception ex)
                {
                    trans.Rollback();
                    throw new Exceptions.SqlException(nameof(ExecuteCommands), _commands);
                }
                finally
                {
                    _cmd.Connection.Close();
                    _commands.Clear();
                }
            }
        }

        public bool IsTransactionComplete() => _isTransactionComplete;
    }
}
