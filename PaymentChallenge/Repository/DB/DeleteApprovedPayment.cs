using PaymentChallenge.Data;
using PaymentChallenge.Data.Enum;
using PaymentChallenge.Repository.Command;
using System.Data.SqlClient;

namespace PaymentChallenge.Repository.DB
{
    public class DeleteApprovedPayment : ICommand
    {
        private Payment _deletePayment;
        public DeleteApprovedPayment(Payment deletePayment)
        {
            _deletePayment = deletePayment;
        }

        public Payment execute(SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = @"delete from ApprovedPayments 
                Where paymentCode=@paymentCode";
            sqlCommand.Parameters.AddWithValue("@PaymentCode", _deletePayment.PaymentCode);
            sqlCommand.ExecuteNonQuery();
            return _deletePayment;
        }
    }
}
