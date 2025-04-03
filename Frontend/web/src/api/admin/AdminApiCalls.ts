const API_BASE_URL = "/admin"; // Admin API:n reittipolku, määritelty backendissä

// Lisää huone
export async function addRoom(roomData: { name: string; defaultOpenDate: string; defaultCloseDate: string }): Promise<{ id: string; name: string }> {
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
export async function addDeviceToRoom(deviceData: { roomId: string; name: string; deviceType: string; description: string }): Promise<string> {
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

    return await response.text(); // Palauttaa onnistumisviestin, esim. "Device added to room successfully."
}