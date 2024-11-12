using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Mvc;
using Presentation.Interfaces;
using Presentation.Proto;
using System.Threading.Tasks;

namespace Presentation.Grpc;

public class GrpcBroker : IGrpcBroker
{

    SpaceService.SpaceServiceClient _client;

    public GrpcBroker(string address) 
    {
        var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions {
            HttpHandler = new GrpcWebHandler(new HttpClientHandler())
        });
        _client = new SpaceService.SpaceServiceClient(channel);

        
    }

    public async Task<Space> GetSpaceById(long id) 
    {
        return await _client.GetSpaceAsync(new SpaceId{ Id = (int) id });
    }

    public async Task<SpaceList> GetSpaceList()
    {
        return await _client.GetSpacesAsync(new Empty{});
    }

    public async Task<Empty> CreateSpace(FormSpace space)
    {
        return await _client.CreateSpaceAsync(space);
    }

    public async Task<Empty> UpdateSpace(Space space)
    {
        return await _client.UpdateSpaceAsync(space);
    }

    public async Task<Empty> DeleteSpace(SpaceId id)
    {
        return await _client.DeleteSpaceAsync(id);
    }

    public async Task<OccupancyList> GetOccupancyList(OccupancyQuery query)
    {
        return await _client.GetAllOccupanciesAsync(query);
    }

    public async Task<Empty> FreeRange(OccupancyRange range)
    {
        return await _client.FreeRangeAsync(range);
    }

    public async Task<Empty> BlockRange(OccupancyRange range)
    {
        return await _client.BlockRangeAsync(range);
    }
}