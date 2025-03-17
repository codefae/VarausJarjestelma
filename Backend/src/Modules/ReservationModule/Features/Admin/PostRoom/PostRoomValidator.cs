using FastEndpoints;
using FluentValidation.Results;

namespace src.Modules.ReservationModule.Features.Admin.PostRoom;

public class PostRoomValidator : Validator<PostRoomRequest>
{
    public PostRoomValidator()
    {
        
    }
}