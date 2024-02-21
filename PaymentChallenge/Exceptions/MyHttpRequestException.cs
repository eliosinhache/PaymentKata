namespace PaymentChallenge.Exceptions
{
    public class MyHttpRequestException : ApplicationException
    {
        public MyHttpRequestException(string name, object key) : base($"Error to connect PaymentChecker Service")
        {

        }
    }
}
