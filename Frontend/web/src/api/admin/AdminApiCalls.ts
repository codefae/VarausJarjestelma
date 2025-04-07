import { PostRoomRequest } from "../../models/postRoomRequest";
import { DeviceDto } from "../../models/deviceDto";

const API_BASE_URL = "/admin"; // Admin API:n reittipolku, määritelty backendissä

// Lisää huone
export async function addRoom(roomData: PostRoomRequest): Promise<{ id: string; name: string }> {
    const response = await fetch(`${API_BASE_URL}/room`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(roomData),
    });

    if (!response.ok) {
        throw new Error(`Failed to add room: ${response.statusText}`);
    }

    return await response.json(); // Palauttaa huoneen tiedot, esim. { id, name }
}

// Lisää laite huoneeseen
export async function addDeviceToRoom(deviceData: DeviceDto & { roomId: string }): Promise<DeviceDto> {
    const response = await fetch(`${API_BASE_URL}/room/device`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(deviceData),
    });

    if (!response.ok) {
        throw new Error(`Failed to add device to room: ${response.statusText}`);
    }

    return await response.json(); // Palauttaa lisätyn laitteen tiedot
}