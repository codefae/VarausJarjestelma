import {useEffect, useState} from 'react';
import { PostDeviceToRoomRequest } from "../models/postDeviceToRoomRequest.ts"; // Adjust the import path if needed
import './ReservationForm.css'; // Reuse the existing CSS file for consistent styling
import { AdminApiCalls } from '../api/admin/AdminApiCalls.ts';
import {UserApiCalls} from "../api/user/UserApiCalls.ts";
import {GetAvailableRoomsResponse} from "../models/getAvailableRoomsResponse.ts";

const PostDeviceToRoomForm = () => {
    const [availableRooms, setAvailableRooms] = useState<GetAvailableRoomsResponse | null>(null);

    useEffect(() => {
        const fetchRooms = async () => {
            try {
                const data = await UserApiCalls.getAvailableRooms();
                setAvailableRooms(data);
            } catch (error) {
                console.error(error);
                alert(error)
            }
        }
        fetchRooms();
    }, []);
    const [formData, setFormData] = useState<PostDeviceToRoomRequest>({
        roomId: "",
        name: "",
        deviceType: "",
        description: "",
    });
    const handleSelectChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { value } = e.target;

        setFormData(prevState => ({
            ...prevState,
            roomId:  value
        }));
    };
    // Handle form field changes
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    // Handle form submission
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await AdminApiCalls.addDeviceToRoom(formData);
            alert("Device added to room successfully!");
        } catch (error) {
            console.error(error);
            alert(error);
        }
    };

    return (
        <div className="container">
            <form onSubmit={handleSubmit} className="reservation-form">
                <h2>Add Device to Room</h2>
                <div className="form-group">
                    <label htmlFor="reservationType">Reservation Type</label>
                    <select
                        id="reservationType"
                        name="reservationType"
                        value={formData.roomId}
                        onChange={handleSelectChange}
                        required
                    >
                        {availableRooms?.availableRooms.map(roomDetails=> (
                            <option value={roomDetails.roomId}>
                                {roomDetails.roomName}
                        </option>))}
                    </select>
                    </div>

                <div className="form-group">
                    <label htmlFor="name">Device Name</label>
                    <input
                        type="text"
                        name="name"
                        id="name"
                        value={formData.name}
                        onChange={handleChange}
                        placeholder="Device Name"
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="deviceType">Device Type</label>
                    <input
                        type="text"
                        name="deviceType"
                        id="deviceType"
                        value={formData.deviceType}
                        onChange={handleChange}
                        placeholder="Device Type"
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="description">Device Description</label>
                    <input
                        type="text"
                        name="description"
                        id="description"
                        value={formData.description}
                        onChange={handleChange}
                        placeholder="Device Description"
                        required
                    />
                </div>

                <button type="submit" className="submit-button">Add Device</button>
            </form>
        </div>
    );
};

export default PostDeviceToRoomForm;
