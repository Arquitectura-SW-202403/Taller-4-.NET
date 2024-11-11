namespace Logica.Services
{
    public class SpaceReservationService
{
    private readonly HttpClientService _httpClientService;

    public SpaceReservationService(HttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    // Crear una nueva reserva
    public async Task<Reservation> CreateReservationAsync(int spaceId, string customerName, DateTime reservationTime)
    {
        var reservationRequest = new
        {
            SpaceId = spaceId,
            CustomerName = owner,
            ReservationTime = reservationTime
        };

        // Aquí, `PostEntityAsync` maneja la solicitud HTTP y te devuelve el resultado.
        return await _httpClientService.PostEntityAsync<Reservation>("reservations", reservationRequest);
    }

    // Cancelar una reserva
    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        return await _httpClientService.DeleteEntityAsync("reservations", reservationId);
    }
}

}
