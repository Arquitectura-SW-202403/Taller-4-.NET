using Presentation.Proto;
namespace Presentation.Interfaces;

public interface IGrpcBroker 
{

    public Task<Space> GetSpaceById(long id);
    public Task<SpaceList> GetSpaceList();
    public Task<Empty> CreateSpace(FormSpace space);
    public Task<Empty> UpdateSpace(Space space);
    public Task<Empty> DeleteSpace(SpaceId id);
}