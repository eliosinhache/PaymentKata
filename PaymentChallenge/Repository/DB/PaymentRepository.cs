using PaymentChallenge.Contract.DB;
using PaymentChallenge.Data;
using PaymentChallenge.Data.Enum;
using System.Data;
using System.Data.SqlClient;

namespace PaymentChallenge.Repository.DB
{
    //public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    public class PaymentRepository : IPaymentRepository
    {
        private SqlCommand _cmd = new SqlCommand();
        public PaymentRepository(IConfiguration config)
        {
            _cmd.Connection = new SqlConnection(config.GetConnectionString("PaymentDB"));
        }

        public bool ApprovedPaymentExists(string paymentCode, float amount)
        {
            try
            {
                _cmd.Parameters.Clear();
                _cmd.CommandText = @"SELECT id
                from ApprovedPayments
                where paymentCode = @paymentCode and amount=@amount";
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@paymentCode", paymentCode);
                _cmd.Parameters.AddWithValue("@amount", amount);
                _cmd.Connection.Open();

                SqlDataReader registros = _cmd.ExecuteReader();
                if (registros.HasRows) { return true; }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }

        public bool Exists(string paymentCode, float amount)
        {
            try
            {
                _cmd.Parameters.Clear();
                _cmd.CommandText = @"SELECT id
                from PaymentState
                where paymentCode = @paymentCode and amount=@amount";
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@paymentCode", paymentCode);
                _cmd.Parameters.AddWithValue("@amount", amount);
                _cmd.Connection.Open();

                SqlDataReader registros = _cmd.ExecuteReader();
                if (registros.HasRows) { return true; }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }

        public Payment GetApproved(string paymentCode)
        {
            try
            {
                _cmd.CommandText = @"SELECT * from ApprovedPayments
				where paymentCode = @paymentCode";
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@paymentCode", paymentCode);
                _cmd.Connection.Open();

                SqlDataReader reg = _cmd.ExecuteReader();
                if (reg.HasRows)
                {
                    reg.Read();
                    Payment payment = new Payment();
                    payment.Id = Convert.ToInt32(reg["id"].ToString());
                    payment.Amount = float.Parse(reg["amount"].ToString());
                    payment.DateEntry = Convert.ToDateTime(reg["dateEntry"]);
                    payment.OperationType = OperationType.Approved;
                    return payment;
                }
                return null;

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }
    }
}
