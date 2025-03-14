using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Features.GetAvailableRooms;

public class GetAvailableRoomsEndpoint(IRoomRepository roomRepository) : EndpointWithoutRequest
<
    Results<Ok<GetAvailableRoomsResponse>, ProblemHttpResult>,
    GetAvailableRoomsMapper
    >
{
    public override void Configure()
    {
        Get("/rooms");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<GetAvailableRoomsResponse>, ProblemHttpResult>> HandleAsync(CancellationToken ct)
    {
       throw new NotImplementedException();
    }
}