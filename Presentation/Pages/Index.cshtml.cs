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
        var res = await _broker.DeleteSpace(
            new Proto.SpaceId { Id = 16 }
        );
        Console.WriteLine(res.ToString());
    }
}
