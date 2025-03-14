using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using src.Modules.ReservationModule.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace src.Modules.ReservationModule.Features.GetAvailableRooms;

public class GetAvailableRoomsEndpoint(IRoomRepository roomRepository) : EndpointWithoutRequest
<
    Results<Ok<GetAvailableRoomsResponse>, ProblemHttpResult>,
    GetAvailableRoomsMapper
>
{
    private readonly IRoomRepository _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));


    public override void Configure()
    {
        Get("/rooms");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<GetAvailableRoomsResponse>, ProblemHttpResult>> HandleAsync(CancellationToken ct)
    {
        try
        {
            var rooms = await _roomRepository.GetRoomsAsync(ct);
            var response = Map.FromEntity(rooms.ToList());
            return TypedResults.Ok(response);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message);
        }
    }
}