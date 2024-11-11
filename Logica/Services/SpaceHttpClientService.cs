using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entidades;

namespace Logica.Services
{
    public class SpaceHttpClientService : IStartupConfigureServicesFilter{
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7251"; 
        public SpaceHttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Metodo de Traer todos los Espacios GetSpaces()
        public async Task<List<Space>> GetSpacesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Space>>($"{_baseUrl}/spaces");
            if (response == null)
            {
                throw new Exception("No se pudieron obtener los espacios.");
            }

            return response;
        }


        // Metodo Obtener Espacios Por Id
        public async Task<Space> GetSpaceByIdAsync(int spaceId)
        {
            var response = await _httpClient.GetFromJsonAsync<Space>($"{_baseUrl}/spaces/{spaceId}");
            if (response == null)
            {
                throw new Exception("No se encontró el espacio.");
            }

            return response;
        }

         // Crear un nuevo espacio
        public async Task<Space> CreateSpaceAsync(Space space)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/spaces", space);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Space>();
        }

        // Obtener todas las reservas de un espacio
        public async Task<List<Reservation>> GetReservationsForSpaceAsync(int spaceId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<Reservation>>($"{_baseUrl}/spaces/{spaceId}/reservations");
            if (response == null)
            {
                throw new Exception("No se encontraron reservas para este espacio.");
            }

            return response;
        }


 



    }
}
