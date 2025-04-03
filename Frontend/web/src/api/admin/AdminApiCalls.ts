const API_BASE_URL = "/admin"; // Admin API:n reittipolku, määritelty backendissä

// Lisää huone
export async function addRoom(roomData: { name: string; defaultOpenDate: string; defaultCloseDate: string }) {
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

    return await response.json();
}

// Lisää laite huoneeseen
export async function addDeviceToRoom(deviceData: { roomId: string; name: string; deviceType: string; description: string }) {
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

    return await response.json();
}

// Hae kaikki huoneet
export async function getRooms() {
    const response = await fetch(`${API_BASE_URL}/rooms`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
    });

    if (!response.ok) {
        throw new Error(`Failed to fetch rooms: ${response.statusText}`);
    }

    return await response.json();
}

// Poista huone
export async function deleteRoom(roomId: string) {
    const response = await fetch(`${API_BASE_URL}/room/${roomId}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        },
    });

    if (!response.ok) {
        throw new Error(`Failed to delete room: ${response.statusText}`);
    }

    return await response.json();
}

// Poista laite huoneesta
export async function deleteDeviceFromRoom(deviceId: string) {
    const response = await fetch(`${API_BASE_URL}/room/device/${deviceId}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        },
    });

    if (!response.ok) {
        throw new Error(`Failed to delete device from room: ${response.statusText}`);
    }

    return await response.json();
}