using FastEndpoints;

namespace src.Modules.ReservationModule.Shared.EndPointGroups;

public class UserEndpointGroup : Group
{
    public UserEndpointGroup()
    {
        Configure(routePrefix: "/user", x =>x.AllowAnonymous());
        
    }
}