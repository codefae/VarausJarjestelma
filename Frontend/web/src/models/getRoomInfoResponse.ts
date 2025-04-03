import {ReservationDto} from "./reservationDto.ts";
import {DeviceDto} from "./deviceDto.ts";
import {OpenRulesDto} from "./openRulesDto.ts";

export interface GetRoomInfoResponse {
    roomId: string;
    roomName: string;
    roomDevices: DeviceDto[];
    openTimes: OpenRulesDto;
    reservationDtos: ReservationDto[];

}











