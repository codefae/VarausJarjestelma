import {PostRoomRequest} from "../../models/postRoomRequest.ts";
import {PostDeviceToRoomRequest} from "../../models/postDeviceToRoomRequest.ts";

const BASE_URL = 'http://192.168.159.23:5121/admin';
const ROOM_BASE_URL = BASE_URL + '/room'; // Admin API:n reittipolku, määritelty backendissä

// Lisää huone
export async function addRoom(postRoomRequest: PostRoomRequest) {
    const response = await fetch(`${ROOM_BASE_URL}/room`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(postRoomRequest),
    });

    if (!response.ok) {
        throw new Error(`Failed to add room: ${response.statusText}`);
    }
}



// Lisää laite huoneeseen
export async function addDeviceToRoom(postDeviceToRoomRequest: PostDeviceToRoomRequest) {
    const response = await fetch(`${ROOM_BASE_URL}/room/device`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(postDeviceToRoomRequest),
    });

    if (!response.ok) {
        throw new Error(`Failed to add device to room: ${response.statusText}`);
    }
}