using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Entidades;

namespace Logica.Services
{
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
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, $"{_baseUrl}/{endpoint}");

            // Si hay datos (en POST o PUT), los agrega al cuerpo de la solicitud
            if (data != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                requestMessage.Content = JsonContent.Create(data);
            }

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                // Si es un DELETE, no esperamos respuesta
                if (method == HttpMethod.Delete)
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
        public async Task<bool> PutEntityAsync<T>(string endpoint, int id, object data)
        {
            var response = await SendRequestAsync<object>(HttpMethod.Put, endpoint, data);
            return response != null;
        }

        // Método para manejar solicitudes DELETE
        public async Task<bool> DeleteEntityAsync(string endpoint, int id)
        {
            await SendRequestAsync<object>(HttpMethod.Delete, $"{endpoint}/{id}");
            return true;
        }

        // Método para crear una reserva sobre un espacio
        public async Task<bool> CreateReservationAsync(int spaceId, string customerName, int start, int end)
        {
            var reservationRequest = new
            {
                spaceId = spaceId,
                Owner = customerName, // Cliente que realiza la reserva
                start = start,
                end
            };

            // PUT request a 'spaces/{spaceId}/reservations' para crear o actualizar la reserva
            var response = await PutEntityAsync<bool>($"spaces/{spaceId}/occupancy_status", spaceId, reservationRequest);
            return response != null; 
        }

        // Método especializado para cancelar una reserva de un espacio
        public async Task<bool> CancelReservationAsync(int spaceId, int reservationId)
        {
            // DELETE request a 'spaces/{spaceId}/reservations/{reservationId}' para cancelar la reserva
            return await DeleteEntityAsync($"spaces/{spaceId}/occupancy_status", reservationId);
        }
    }
}
