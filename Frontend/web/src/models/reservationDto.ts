import {TimeSlotDto} from "./timeSlotDto.ts";

export interface ReservationDto {
    roomId: string;
    reservationType: string;
    timeSlotDto: TimeSlotDto;
    day: Date;
    deviceId?: string; // Optional, equivalent to `[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]`
}