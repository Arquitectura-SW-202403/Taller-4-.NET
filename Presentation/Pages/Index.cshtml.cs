using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Grpc;
using Presentation.Interfaces;

namespace Presentation.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private GrpcBroker _broker;
    
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _broker = new GrpcBroker("https://localhost:7125");
    }

    public async void OnGet()
    {
        /*var res = await _broker.GetOccupancyList(
            new Proto.OccupancyQuery {
                SpaceId=17,
                Start=6,
                End=20
            }
        );*/

        /*var res = await _broker.BlockRange(
            new Proto.OccupancyRange {
                SpaceId=17,
                Start=6,
                End=10,
                Owner="Galindo"
            }
        );*/

        /*var res = await _broker.FreeRange(
            new Proto.OccupancyRange {
                SpaceId=17,
                Start=6,
                End=10,
                Owner="Galindo"
            }
        );*/

        var res = await _broker.GetZonesList();
        Console.WriteLine(res.ToString());
    }
}
