namespace PaymentChallenge.Repository.ExternalServices
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            // Configura aquí opciones adicionales para HttpClient si es necesario
        }

        public async Task<string> GetResourceFromOtherApi(string apiUrl)
        {
            try
            {
                // Realiza una solicitud GET al endpoint de la otra API REST
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                // Asegúrate de que la solicitud fue exitosa antes de procesar la respuesta
                response.EnsureSuccessStatusCode();

                // Lee el contenido de la respuesta como una cadena
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (HttpRequestException ex)
            {
                // Maneja cualquier error de solicitud HTTP aquí
                Console.WriteLine($"Error al realizar la solicitud HTTP: {ex.Message}");
                throw;
            }
        }
    }
}
