using System;
using System.Threading.Tasks;
using src.Modules.ReservationModule.Shared.Interfaces;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

namespace src.Modules.ReservationModule.Features.PatchReservation
{
    public class PatchReservationTime
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public PatchReservationTime(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<(Reservation reservation, Room room)> LoadReservationAndRoomAsync(int reservationId)
        {
            // Load the reservation object
            var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                throw new Exception($"Reservation with ID {reservationId} not found.");
            }

            // Load the room object based on the reservation
            var room = await _roomRepository.GetRoomByIdAsync(reservation.RoomId);
            if (room == null)
            {
                throw new Exception($"Room with ID {reservation.RoomId} not found.");
            }

            return (reservation, room);
        }
    }
}