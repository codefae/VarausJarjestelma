import {TimeSlotDto} from "./timeSlotDto.ts";

export interface ReservationDto {
    id: string | null;
    roomId: string;
    reservationType: string;
    timeSlotDto: TimeSlotDto;
    day: Date;
    deviceId: string | null; // Optional, equivalent to `[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]`
}