using PaymentChallenge.Data;
using PaymentChallenge.Data.Enum;
using PaymentChallenge.Repository.Command;
using System.Data.SqlClient;

namespace PaymentChallenge.Repository.DB
{
    public class NewPaymentRecord : ICommand
    {
        private Payment _newPayment;
        private OperationType _operationType;
        public NewPaymentRecord(Payment newPayment, OperationType operationType)
        {
            this._newPayment = newPayment;
            this._operationType = operationType;
        }
        public Payment execute(SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = @"insert into PaymentState (clientId, amount, dateEntry, paymentCode,stateId)
                Values (@clientId, @amount, @dateEntry, @paymentCode, @stateId)" +
                "SELECT CAST(scope_identity() AS int)";
            sqlCommand.Parameters.AddWithValue("@clientId", _newPayment.ClientId);
            sqlCommand.Parameters.AddWithValue("@amount", _newPayment.Amount);
            sqlCommand.Parameters.AddWithValue("@dateEntry", DateTime.Now);
            sqlCommand.Parameters.AddWithValue("@paymentCode", _newPayment.PaymentCode);
            sqlCommand.Parameters.AddWithValue("@stateId", _operationType);
            _newPayment.Id = Convert.ToInt32(sqlCommand.ExecuteScalar());
            return _newPayment;
        }
    }
}
