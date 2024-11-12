using Entidades;
using Grpc.Core;
using Google.Protobuf;
using Logica.Proto;
using Org.BouncyCastle.Crypto.Prng;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage;
namespace Logica.Services;

public class SpaceServiceImpl : SpaceService.SpaceServiceBase
{

    private HttpClientService _httpClient;

    public SpaceServiceImpl() {
        _httpClient = new HttpClientService(new HttpClient(), "https://localhost:7251");
    }

    public override async Task<Empty> CreateSpace(FormSpace request, ServerCallContext context)
    {
        var obj = new {
            name = request.Name,
            description = request.Description,
            capacity = request.Capacity,
            zone_id = request.ZoneId
        };
        await _httpClient.PostEntityAsync<object>("api/space", obj);
        return new Empty{};
    }

    public override async Task<Proto.Space> GetSpace(SpaceId request, ServerCallContext context)
    {
        var f = (JsonElement) await _httpClient.GetEntityAsync<object>("api/space", request.Id);
        return (Proto.Space) JsonParser.Default.Parse(f.ToString(), Proto.Space.Descriptor);
    }

    public override async Task<Empty> UpdateSpace(Proto.Space request, ServerCallContext context)
    {

        var obj = new {
            id = request.Id,
            name = request.Name,
            description = request.Description,
            capacity = request.Capacity,
            zone_id = request.ZoneId
        };

        await _httpClient.PutEntityAsync<object>("api/space", (int)request.Id, obj);

        return new Empty{};
    }

    public override async Task<SpaceList> GetSpaces(Empty request, ServerCallContext context)
    {
        var f = (JsonElement) await _httpClient.GetListAsync<object>("api/space");
        return (SpaceList) JsonParser.Default.Parse(f.ToString(), SpaceList.Descriptor);
    }

    public override async Task<Empty> DeleteSpace(SpaceId request, ServerCallContext context)
    {
        await _httpClient.DeleteEntityAsync("api/space", request.Id);
        return new Empty{};
    }

    // ------------------------------------------

    public override async Task<OccupancyList> GetAllOccupancies(OccupancyQuery request, ServerCallContext context)
    {
        Dictionary<string, string> queries = new Dictionary<string, string>();
        queries["start"] = request.Start.ToString();
        queries["end"] = request.End.ToString();
        var f = (JsonElement) await _httpClient.GetEntityAsyncQuery<object>("api/OccupancyStatus",(int)request.SpaceId, queries);

        return (OccupancyList) JsonParser.Default.Parse(f.ToString(), OccupancyList.Descriptor);
    }

    public override async Task<Empty> BlockRange(OccupancyRange request, ServerCallContext context)
    {
        var obj = new {
            owner = request.Owner,
            start = request.Start,
            end = request.End
        };
        await _httpClient.PostEntityAsync<object>($"api/OccupancyStatus/block/{request.SpaceId}", obj);
        return new Empty{};
    }

    public override async Task<Empty> FreeRange(OccupancyRange request, ServerCallContext context)
    {
        var obj = new {
            owner = request.Owner,
            start = request.Start,
            end = request.End
        };
        await _httpClient.PostEntityAsync<object>($"api/OccupancyStatus/free/{request.SpaceId}", obj);
        return new Empty{};
    }
}
