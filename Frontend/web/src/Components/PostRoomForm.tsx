import { useState } from 'react';
import { PostRoomRequest } from "../models/postRoomRequest.ts";
import './ReservationForm.css'; // Reuse the existing CSS file for consistent styling
import {AdminApiCalls} from '../api/admin/AdminApiCalls.ts'

const PostRoomForm = () => {
    const [formData, setFormData] = useState<PostRoomRequest>({
        name: "",
        defaultOpenDate: new Date(),
        defaultCloseDate: new Date()
    });

    // Handle form field changes
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: name.includes("Date") ? new Date(value) : value
        }));
    };

    // Handle form submission
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await AdminApiCalls.addRoom(formData)
            alert("Room created successfully!");
        } catch (error) {
            console.error(error);
            alert(error);
        }
    };

    return (
        <div className="container">
            <form onSubmit={handleSubmit} className="reservation-form">
                <h2>Create a Room</h2>

                <div className="form-group">
                    <label htmlFor="name">Room Name</label>
                    <input
                        type="text"
                        name="name"
                        id="name"
                        value={formData.name}
                        onChange={handleChange}
                        placeholder="Room Name"
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="defaultOpenDate">Default Open Date</label>
                    <input
                        type="date"
                        name="defaultOpenDate"
                        id="defaultOpenDate"
                        value={formData.defaultOpenDate.toISOString().split("T")[0]}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="defaultCloseDate">Default Close Date</label>
                    <input
                        type="date"
                        name="defaultCloseDate"
                        id="defaultCloseDate"
                        value={formData.defaultCloseDate.toISOString().split("T")[0]}
                        onChange={handleChange}
                        required
                    />
                </div>

                <button type="submit" className="submit-button">Create Room</button>
            </form>
        </div>
    );
};

export default PostRoomForm;
