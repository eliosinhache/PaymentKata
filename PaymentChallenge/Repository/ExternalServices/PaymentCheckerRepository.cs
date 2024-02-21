using PaymentChallenge.Contract.ExternalServices;

namespace PaymentChallenge.Repository.ExternalServices
{
    public class PaymentCheckerRepository : IPaymentCheckerService
    {

        private readonly HttpClient _httpClient;

        public PaymentCheckerRepository()
        {
            _httpClient = new HttpClient();
            // Configura aquí opciones adicionales para HttpClient si es necesario
        }
        public async Task<bool> IsPaymentValid(float amount)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:8090/Checker?payment={amount.ToString().Replace(',', '.')}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody == "Pago autorizado" ? true : false;
            }
            catch (HttpRequestException ex)
            {
                throw new PaymentChallenge.Exceptions.MyHttpRequestException(nameof(IsPaymentValid), amount);
            }
    }
    }
}
