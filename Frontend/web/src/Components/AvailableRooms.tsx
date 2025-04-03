import { useState, useEffect } from "react";

export interface GetAvailableRoomsResponse {
    availableRooms: RoomDetails[];
}

const AvailableRooms = () => {
    const [availableRooms, setAvailableRooms] = useState<RoomDetails[]>([]);

    useEffect(() => {
        const fetchRooms = async () => {
            try {
                const response = await fetch("/api/rooms");
                if (!response.ok) throw new Error("Failed to fetch rooms");
                const data: GetAvailableRoomsResponse = await response.json();
                setAvailableRooms(data.availableRooms);
            } catch (error) {
                console.error(error);
            }
        };

        fetchRooms();
    }, []);

    return (
        <div>
            <h2>Available Rooms</h2>
            <ul>
                {availableRooms.length === 0 ? (
                    <p>No rooms available</p>
                ) : (
                    availableRooms.map(room => (
                        <li key={room.roomId}>{room.roomName}</li>
                    ))
                )}
            </ul>
        </div>
    );
};

export default AvailableRooms;
