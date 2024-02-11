using PaymentChallenge.Data;
using PaymentChallenge.Data.Enum;
using PaymentChallenge.Repository.Command;
using System.Data.SqlClient;

namespace PaymentChallenge.Repository.DB
{
    public class NewApprovedPaymentRecord : ICommand
    {
        private Payment approvedPayment;
        public NewApprovedPaymentRecord(Payment approvedPayment)
        {
            this.approvedPayment = approvedPayment;
        }

        public Payment execute(SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = @"insert into ApprovedPayments (clientId, amount, dateEntry, PaymentCode)
                Values (@clientId, @amount, @dateEntry, @PaymentCode)" +
                "SELECT CAST(scope_identity() AS int)";
            sqlCommand.Parameters.AddWithValue("@clientId", approvedPayment.ClientId);
            sqlCommand.Parameters.AddWithValue("@amount", approvedPayment.Amount);
            sqlCommand.Parameters.AddWithValue("@dateEntry", DateTime.Now);
            sqlCommand.Parameters.AddWithValue("@PaymentCode", approvedPayment.PaymentCode);
            approvedPayment.Id = Convert.ToInt32(sqlCommand.ExecuteScalar());
            return approvedPayment;
        }
    }
}
