import {TimeSlotDto} from "./timeSlotDto.ts";

export interface WeeklyScheduleDto {
    monday: TimeSlotDto;
    tuesday: TimeSlotDto;
    wednesday: TimeSlotDto;
    thursday: TimeSlotDto;
    friday: TimeSlotDto;
    saturday: TimeSlotDto;
    sunday: TimeSlotDto;
}