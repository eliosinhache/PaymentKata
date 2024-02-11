using PaymentChallenge.Data;
using System.Data.SqlClient;
namespace PaymentChallenge.Repository.Command
{
    public interface ICommand
    {
        Payment execute(SqlCommand sqlCommand);
    }
}
