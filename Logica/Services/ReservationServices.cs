public class ReservationService
{
    private readonly HttpClientService _httpClientService;

    public ReservationService(HttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<bool> CreateReservationAsync(int spaceId, string customerName, DateTime reservationTime)
    {
        var reservationRequest = new
        {
            spaceId=spaceId,
            Owner = customerName, 
            ReservationTime = reservationTime
        };

        // Se realiza una solicitud POST para crear la reserva sobre el espacio
        return await _httpClientService.PutEntityAsync<bool>($"spaces/{spaceId}/occupancy_status", reservationRequest);
    }

    public async Task<bool> CancelReservationAsync(int spaceId, int reservationId)
    {
        // Aquí se hace una solicitud DELETE para cancelar la reserva de un espacio específico
        return await _httpClientService.DeleteEntityAsync($"spaces/{spaceId}/occupancy_status", reservationId);
    }
}
