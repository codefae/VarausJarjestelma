export interface GetAvailableRoomsResponse {
    availableRooms: RoomDetails[];
}

export interface RoomDetails {
    roomId: string;
    roomName: string;
}
