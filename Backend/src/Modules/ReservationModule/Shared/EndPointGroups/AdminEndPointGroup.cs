using FastEndpoints;

namespace src.Modules.ReservationModule.Shared.EndPointGroups;

public class AdminEndpointGroup : Group
{
    public AdminEndpointGroup()
    {
        Configure(routePrefix: "/admin", x =>x.AllowAnonymous());
        
    }
}