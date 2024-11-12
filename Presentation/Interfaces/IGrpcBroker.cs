using Presentation.Proto;
namespace Presentation.Interfaces;

public interface IGrpcBroker 
{

    public Task<Space> GetSpaceById(long id);
}