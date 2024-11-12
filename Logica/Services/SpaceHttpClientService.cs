
namespace Logica.Services;
public class HttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public HttpClientService(HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl;
    }

    // Método Macro - (Con este vamos a manejar todas las request)
    public async Task<T> SendRequestAsync<T>(HttpMethod method, string endpoint, object data = null)
    {

        Console.WriteLine($"{_baseUrl}/{endpoint}");
        HttpRequestMessage requestMessage = new HttpRequestMessage(method, $"{_baseUrl}/{endpoint}");

        // Si hay datos (en POST o PUT), los agrega al cuerpo de la solicitud
        if (data != null && (method == HttpMethod.Post || method == HttpMethod.Put))
        {
            requestMessage.Content = JsonContent.Create(data);
            Console.WriteLine((await requestMessage.Content.ReadAsStringAsync()).ToString());
        }

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

        if (response.IsSuccessStatusCode)
        {
            // Si es un DELETE, no esperamos respuesta
            if (method != HttpMethod.Get)
            {
                return default; // Para DELETE no hay respuesta, se devuelve Null
            }
            return await response.Content.ReadFromJsonAsync<T>(); // Devuelve la respuesta parseada
        }
        else
        {
            // Lanza una excepción si paila
            throw new Exception($"Error en la operación HTTP: {response.ReasonPhrase}");
        }
    }

    // Método para manejar solicitudes GET (Listas solamente)
    public async Task<T> GetListAsync<T>(string endpoint)
    {
        return await SendRequestAsync<T>(HttpMethod.Get, endpoint);
    }

    // Método para manejar solicitudes GET para una sola entidad
    public async Task<T> GetEntityAsync<T>(string endpoint, int id)
    {
        return await SendRequestAsync<T>(HttpMethod.Get, $"{endpoint}/{id}");
    }


    // Método para manejar solicitudes POST para una sola entidad
    public async Task<object> PostEntityAsync<T>(string endpoint, object entity)
    {
        return await SendRequestAsync<object>(HttpMethod.Post, endpoint, entity);
    }
    

    // Método para manejar solicitudes PUT
    public async Task<object> PutEntityAsync<T>(string endpoint, int id, object data)
    {
        var response = await SendRequestAsync<object>(HttpMethod.Put, $"{endpoint}/{id}", data);
        return response;
    }

    // Método para manejar solicitudes DELETE
    public async Task<bool> DeleteEntityAsync(string endpoint, int id)
    {
        await SendRequestAsync<object>(HttpMethod.Delete, $"{endpoint}/{id}");
        return true;
    }


}
