import {ReservationDto} from "./reservationDto.ts";

export interface PostReservationRequest {
    userId: string;
    reservationDto: ReservationDto;
}
