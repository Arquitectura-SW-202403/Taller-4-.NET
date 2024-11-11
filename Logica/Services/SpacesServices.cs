using Grpc.Core;
using Logica;

namespace Logica.Services
{
    public class SpaceService
{
    private readonly HttpClientService _httpClientService;

    public SpaceService(HttpClient httpClient)
    {
        _httpClientService = new HttpClientService(httpClient, "https://localhost:7251");
    }

    public async Task<List<Space>> GetSpacesAsync()
    {
        return await _httpClientService.GetListAsync<Space>("spaces");
    }

    public async Task<Space> GetSpaceByIdAsync(int spaceId)
    {
        return await _httpClientService.GetEntityAsync<Space>("spaces", spaceId);
    }

    public async Task<Space> CreateSpaceAsync(Space space)
    {
        return await _httpClientService.PostEntityAsync("spaces",space);
    }

    //public async Task<Space> UpdateSpaceAsync(int spaceId, Space space)
    //{
    //    return await _httpClientService.PutEntityAsync("spaces", spaceId, space);
    //}
    //

    public async Task<bool> DeleteSpaceAsync(int spaceId)
    {
        return await _httpClientService.DeleteEntityAsync("spaces", spaceId);
    }

}

}
