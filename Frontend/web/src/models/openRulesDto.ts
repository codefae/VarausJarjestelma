import {WeeklyScheduleDto} from "./WeeklyScheduleDto.ts";
import {OpenTimeForDayDto} from "./OpenTimeForDayDto.ts";

export interface OpenRulesDto {
    defaultOpenDate: Date;
    defaultCloseDate: Date;
    openTimesSingleDays: OpenTimeForDayDto[];
    defaultOpenTimesForWeek: WeeklyScheduleDto;
}