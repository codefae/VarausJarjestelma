import {OpenRulesDto} from "./openRulesDto.ts";

export interface PatchOpenRulesForRoomRequest {
    roomId: string;
    openRules: OpenRulesDto
}
