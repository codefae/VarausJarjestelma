import { useState } from "react";
import {PostRoomRequest} from "../models/postRoomRequest.ts";

const PostRoomForm = () => {
    const [formData, setFormData] = useState<PostRoomRequest>({
        name: "",
        defaultOpenDate: new Date(),
        defaultCloseDate: new Date()
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: name.includes("Date") ? new Date(value) : value
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await fetch("/api/rooms", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(formData)
            });
            if (!response.ok) throw new Error("Failed to create room");
            alert("Room created successfully!");
        } catch (error) {
            console.error(error);
            alert("Error creating room");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                name="name"
                value={formData.name}
                onChange={handleChange}
                placeholder="Room Name"
                required
            />
            <input
                type="date"
                name="defaultOpenDate"
                value={formData.defaultOpenDate.toISOString().split("T")[0]}
                onChange={handleChange}
                required
            />
            <input
                type="date"
                name="defaultCloseDate"
                value={formData.defaultCloseDate.toISOString().split("T")[0]}
                onChange={handleChange}
                required
            />
            <button type="submit">Create Room</button>
        </form>
    );
};

export default PostRoomForm;
