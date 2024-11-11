using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Logica.Services{
    public class HttpClientService{
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public HttpClientService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        // Metodo Macro - (Con este vamos a manejar todas las request)
        public async Task<T> SendRequestAsync<T>(HttpMethod method, string endpoint, object data = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, $"{_baseUrl}/{endpoint}");

            if (data != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                requestMessage.Content = JsonContent.Create(data);
            }

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                if (method == HttpMethod.Delete)
                {
                    return default; // Para DELETE no hay respuesta, se devuelve Null
                }
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                throw new Exception($"Error en la operación HTTP: {response.ReasonPhrase}");
            }
        }

        // Método para manejar solicitudes GETb(Listas solamenbte)
        public async Task<List<T>> GetListAsync<T>(string endpoint)
        {
            return await SendRequestAsync<List<T>>(HttpMethod.Get, endpoint);
        }

        // Método para manejar solicitudes GET para una sola entidad
        public async Task<T> GetEntityAsync<T>(string endpoint, int id)
        {
            return await SendRequestAsync<T>(HttpMethod.Get, $"{endpoint}/{id}");
        }

        // Método para manejar solicitudes POST
        public async Task<T> PostEntityAsync<T>(string endpoint, T entity)
        {
            return await SendRequestAsync<T>(HttpMethod.Post, endpoint, entity);
        }

        // Método para manejar solicitudes PUT
        public async Task<T> PutEntityAsync<T>(string endpoint, int id, T entity)
        {
            return await SendRequestAsync<T>(HttpMethod.Put, $"{endpoint}/{id}", entity);
        }

        // Método  para manejar solicitudes DELETE
        public async Task<bool> DeleteEntityAsync(string endpoint, int id)
        {
            await SendRequestAsync<object>(HttpMethod.Delete, $"{endpoint}/{id}");
            return true; 
        }


    }
}