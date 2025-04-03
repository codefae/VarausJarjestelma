import {TimeSlotDto} from "./timeSlotDto.ts";

export interface OpenTimeForDayDto {
    day: Date;
    timeSlotDto: TimeSlotDto;
}