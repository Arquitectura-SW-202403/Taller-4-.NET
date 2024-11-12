using Logica.Services;

public class ReservationService
{
    private readonly HttpClientService _httpClientService;

    public ReservationService(HttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<object> CreateReservationAsync(int spaceId, string customerName, int start, int end)
        {
            var reservationRequest = new
            {
                Owner = customerName, // Cliente que realiza la reserva
                start = start,
                end = end
            };

            
            var response = await _httpClientService.PostEntityAsync<object>($"api/occupancy_status/{spaceId}",reservationRequest);
            return response; 
        }

    public async Task<bool> CancelReservationAsync(int spaceId, int reservationId)
    {

        return await _httpClientService.DeleteEntityAsync($"spaces/{spaceId}/occupancy_status", reservationId);
    }
}
