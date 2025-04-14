import {useEffect, useState} from 'react';
import {GetAvailableRoomsResponse, RoomDetails} from "../models/getAvailableRoomsResponse.ts";
import {GetRoomInfoResponse} from "../models/getRoomInfoResponse.ts";
import {UserApiCalls} from "../api/user/UserApiCalls.ts";
import ReservationForm from "./ReservationForm.tsx";
import './AvailableRooms.css'
import Modal from "./Modal.tsx";
import {AdminApiCalls} from "../api/admin/AdminApiCalls.ts";
import Admin from "../routes/Admin.tsx";

interface AvailableRoomsProps {
    isAdmin: boolean
}

const AvailableRooms = ({isAdmin}: AvailableRoomsProps) => {
    const [availableRooms, setAvailableRooms] = useState<GetAvailableRoomsResponse | null>(null);
    const [roomInfo, setRoomInfo] = useState<GetRoomInfoResponse | null>(null);
    const [loading, setLoading] = useState(true);
    const [loadingRoomInfo, setLoadingRoomInfo] = useState(false);
    const [modalOpen, setModalOpen] = useState(false);
    const fetchRooms = async () => {
        try {
            const data = await UserApiCalls.getAvailableRooms();
            setAvailableRooms(data);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    };
    // Fetch available rooms when component mounts
    useEffect(() => {
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

    const deleteRoom = async (roomId: string) => {
        setLoading(true);
        try {
            await AdminApiCalls.deleteRoom(roomId);
        } catch (error) {
            alert(error)
            console.error(`Error fetching room info for ID ${roomId}:`, error);
        } finally {
            alert("Successfully deleted room")
            await fetchRooms()
            setLoading(false);
        }
    }

    const deleteReservation = async (reservationId: string, roomId: string) => {
        setLoadingRoomInfo(true);
        try {
            await UserApiCalls.deleteReservation(reservationId);
        } catch (error) {
            alert(error)
            console.error('Error deleting reservatino');
        } finally {
            fetchRoomInfo(roomId);
        }
    }

    const infoButtonClicked = (room: RoomDetails) => {
        fetchRoomInfo(room.roomId)
        setModalOpen(true)
    }

    const reservationAdded = () => {
        fetchRoomInfo(roomInfo!.roomId!)
    }

    const deleteDeviceFromRoom =  async ( roomId: string, deviceId: string) => {
        setLoadingRoomInfo(true);
        try {
            await AdminApiCalls.deleteDeviceFromRoom(roomId, deviceId);
        } catch (error) {
            alert(error)
            console.error('Error deleting device');
        } finally {
            fetchRoomInfo(roomId);
        }
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
                            <button className="info-button" onClick={() => infoButtonClicked(room)}>
                                Get Room Info
                            </button>
                            {isAdmin ? <button onClick={() => deleteRoom(room.roomId)}>Delete</button> : null}
                        </li>
                    ))}
                </ul>
            )}

            {loadingRoomInfo ? (
                <p>Loading room details...</p>
            ) : roomInfo ? (


                <Modal isOpen={modalOpen} onClose={() => setModalOpen(false)}>
                    <div className="modal">
                        <div className="room-details">
                            <h3>Room Details</h3>
                            <p><strong>Room name: </strong>{roomInfo.roomName}</p>
                            <p><strong>Opening
                                date: </strong>{roomInfo.openTimes.defaultOpenDate.toString().split('T')[0]}</p>
                            <p><strong>Closing
                                date: </strong>{roomInfo.openTimes.defaultCloseDate.toString().split('T')[0]}</p>
                            <p>
                                <strong>Monday: </strong>{roomInfo.openTimes.defaultOpenTimesForWeek.monday.startTime} - {roomInfo.openTimes.defaultOpenTimesForWeek.monday.endTime}
                            </p>
                            <p>
                                <strong>Tuesday: </strong>{roomInfo.openTimes.defaultOpenTimesForWeek.tuesday.startTime} - {roomInfo.openTimes.defaultOpenTimesForWeek.tuesday.endTime}
                            </p>
                            <p>
                                <strong>Wednesday: </strong>{roomInfo.openTimes.defaultOpenTimesForWeek.wednesday.startTime} - {roomInfo.openTimes.defaultOpenTimesForWeek.wednesday.endTime}
                            </p>
                            <p>
                                <strong>Thursday: </strong>{roomInfo.openTimes.defaultOpenTimesForWeek.thursday.startTime} - {roomInfo.openTimes.defaultOpenTimesForWeek.thursday.endTime}
                            </p>
                            <p>
                                <strong>Friday: </strong>{roomInfo.openTimes.defaultOpenTimesForWeek.friday.startTime} - {roomInfo.openTimes.defaultOpenTimesForWeek.friday.endTime}
                            </p>
                            <p>
                                <strong>Saturday: </strong>{roomInfo.openTimes.defaultOpenTimesForWeek.saturday.startTime} - {roomInfo.openTimes.defaultOpenTimesForWeek.saturday.endTime}
                            </p>
                            <p>
                                <strong>Sunday: </strong>{roomInfo.openTimes.defaultOpenTimesForWeek.sunday.startTime} - {roomInfo.openTimes.defaultOpenTimesForWeek.sunday.endTime}
                            </p>

                            {roomInfo.roomDevices.length != 0 ?
                                (<>
                                <h3>Devices</h3>
                                {roomInfo.roomDevices.map(device => (
                                    <div style={{ padding: "30px ",
                                        margin: "20px",
                                        border: "1px solid black",
                                        borderRadius: "5px"}}>
                                        <p><strong>Name: </strong>{device.name}</p>
                                        <p><strong>Device type: </strong>{device.deviceType}</p>
                                        <p><strong>Description: </strong>{device.description}</p>

                                        {isAdmin ? <button onClick={() => deleteDeviceFromRoom( roomInfo?.roomId, device.id)}>Delete</button> : null}
                                    </div>
                                ))}
                                </>
                                ):null}

                                {roomInfo.openTimes.openTimesSingleDays.length !== 0 ?
                                    <>
                                        <h3>Exceptions</h3>
                                        {roomInfo.openTimes.openTimesSingleDays.map(openTime => (
                                            <p><strong></strong>{openTime.day.toString().split('T')[0]}</p>
                                        ))}
                                    </> : null
                                }
                                {roomInfo.reservationDtos.length != 0 ? (
                                    <>
                                        <h3>Reservations</h3>
                                        <ul>
                                            {roomInfo.reservationDtos.map(reservation => (
                                                <div style={{
                                                    padding: "30px ",
                                                    margin: "20px",
                                                    border: "1px solid black",
                                                    borderRadius: "5px"
                                                }}>
                                                    <p><strong>Reservation type: </strong>{reservation.reservationType}
                                                    </p>
                                                    <p><strong>Date: </strong>{reservation.day.toString().split('T')[0]}
                                                    </p>
                                                    <p><strong>Start time: </strong>{reservation.timeSlotDto.startTime}
                                                    </p>
                                                    <p><strong>End time: </strong>{reservation.timeSlotDto.endTime}</p>
                                                    {reservation.reservationType == "DeviceReservation" ?
                                                        <>
                                                        <p><strong>Device
                                                            name: </strong> {roomInfo.roomDevices.find(x => x.id == reservation.deviceId)?.name}
                                                        </p>

                                                </>: <></>}
                                                    {isAdmin ? <button
                                                        onClick={() => deleteReservation(reservation.id!, reservation.roomId)}>Delete</button> : null}
                                                </div>
                                            ))}
                                        </ul>
                                    </>) : null}

                                </div>
                                <ReservationForm roomId={roomInfo.roomId} devices={roomInfo.roomDevices}
                                                 reservationAdded={reservationAdded}/>
                                </div>
                                </Modal>


                                ) : null}
                        </div>
                        );
                        };


                        export default AvailableRooms;
