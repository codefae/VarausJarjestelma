import { useEffect, useState } from 'react';
import {GetAvailableRoomsResponse, RoomDetails} from "../models/getAvailableRoomsResponse.ts";
import {GetRoomInfoResponse} from "../models/getRoomInfoResponse.ts";
import {UserApiCalls} from "../api/user/UserApiCalls.ts";
import ReservationForm from "./ReservationForm.tsx";
import './AvailableRooms.css'
import Modal from "./Modal.tsx";

const AvailableRooms = () => {
    const [availableRooms, setAvailableRooms] = useState<GetAvailableRoomsResponse | null>(null);
    const [roomInfo, setRoomInfo] = useState<GetRoomInfoResponse | null>(null);
    const [loading, setLoading] = useState(true);
    const [loadingRoomInfo, setLoadingRoomInfo] = useState(false);
    const [modalOpen, setModalOpen] = useState(false);

    // Fetch available rooms when component mounts
    useEffect(() => {
        const fetchRooms = async () => {
            try {
                const data = await UserApiCalls.getAvailableRooms();
                setAvailableRooms(data);
            } catch (error) {
                console.error(error);
                alert(error)
            } finally {
                setLoading(false);
            }
        };

        fetchRooms();
    }, []);

    // Fetch detailed info for a specific room by roomId
    const fetchRoomInfo = async (roomId: string) => {
        setLoadingRoomInfo(true);
        try {
            const data = await UserApiCalls.getRoomInfo(roomId);
            setRoomInfo(data);
        } catch (error) {
            alert(error)
            console.error(`Error fetching room info for ID ${roomId}:`, error);
        } finally {
            setLoadingRoomInfo(false);
        }
    };

    const infoButtonClicked = (room: RoomDetails) => {
        fetchRoomInfo(room.roomId)
        setModalOpen(true)
    }
    return (
        <div className="container">
            <h2>Available Rooms</h2>

            {loading ? (
                <p>Loading rooms...</p>
            ) : !availableRooms?.availableRooms?.length ? (
                <p>No rooms available</p>
            ) : (
                <ul className="room-list">
                    {availableRooms.availableRooms.map(room => (
                        <li key={room.roomId} className="room-item">
                            <div className="room-name">
                                {room.roomName}
                            </div>
                            <button className="info-button" onClick={() =>infoButtonClicked(room)}>
                                Get Room Info
                            </button>
                        </li>
                    ))}
                </ul>
            )}

            {loadingRoomInfo ? (
                <p>Loading room details...</p>
            ) : roomInfo ? (


                    <Modal isOpen={modalOpen} onClose={() => setModalOpen(false)}>
                        <div  className="modal">
                        <div className="room-details">
                            <h3>Room Details</h3>
                            <p><strong>Room Name:</strong> {roomInfo.roomName}</p>
                            <p><strong>Capacity:</strong> {}</p>
                            <p><strong>Location:</strong> {}</p>
                            <ul >
                                {roomInfo.reservationDtos.map(reservation => (
                                    <li>
                                        <p><strong>Reservation type: </strong>{reservation.reservationType}</p>
                                        <p><strong>Date: </strong>{reservation.day.toString().split('T')[0]}</p>
                                        <p><strong>Start time: </strong>{reservation.timeSlotDto.startTime}</p>
                                        <p><strong>End time: </strong>{reservation.timeSlotDto.endTime}</p>
                                        {reservation.reservationType == "DeviceReservation"?<p><strong>Device id:</strong> {reservation.deviceId}</p>:<></>}
                                    </li>
                                ))}
                            </ul>
                        </div>
                        <ReservationForm roomId={roomInfo.roomId} />
                        </div>
                    </Modal>


            ) : null}
        </div>
    );
};


export default AvailableRooms;
