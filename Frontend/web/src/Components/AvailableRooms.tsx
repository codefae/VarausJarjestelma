import { useState, useEffect } from "react";
import {GetAvailableRoomsResponse} from "../models/getAvailableRoomsResponse.ts";


const AvailableRooms = () => {
    const [availableRooms, setAvailableRooms] = useState<GetAvailableRoomsResponse>();

    useEffect(() => {
        const fetchRooms = async () => {
            try {

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
