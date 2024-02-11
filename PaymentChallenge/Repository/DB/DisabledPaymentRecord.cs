using PaymentChallenge.Data;
using PaymentChallenge.Data.Enum;
using PaymentChallenge.Repository.Command;
using System.Data;
using System.Data.SqlClient;

namespace PaymentChallenge.Repository.DB
{
    public class DisabledPaymentRecord : ICommand
    {
        private Payment _payment;
        public DisabledPaymentRecord(Payment payment)
        {
            _payment = payment;
        }
        public Payment execute(SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = @"Update PaymentState SET isActive = @isActive
                where PaymentCode = @PaymentCode";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.AddWithValue("@PaymentCode", _payment.PaymentCode);
            sqlCommand.Parameters.AddWithValue("@isActive", 0);
            sqlCommand.ExecuteNonQuery();
            return _payment;
        }
    }
}
