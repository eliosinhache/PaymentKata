namespace PaymentChallenge.Exceptions
{
    public class SqlException : ApplicationException
    {
        public SqlException(string name, object key) : base($"Error to performance the operation {name} on the DB")
        {

        }
    }
}
