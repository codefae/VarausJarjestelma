import { useState } from "react";

import './ReservationForm.css';
import { AdminApiCalls } from "../api/admin/AdminApiCalls.ts";
import {WeeklyScheduleDto} from "../models/WeeklyScheduleDto.ts";
import {TimeSlotDto} from "../models/timeSlotDto.ts";
import {PatchOpenRulesForRoomRequest} from "../models/PatchOpenRulesForRoomRequest.ts";

const daysOfWeek: (keyof WeeklyScheduleDto)[] = [
    "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"
];

const defaultTimeSlot: TimeSlotDto = {
    startTime: "08:00:00",
    endTime: "16:00:00"
};

const PatchOpenRulesForm = () => {
    const [roomId, setRoomId] = useState<string>("");
    const [weeklySchedule, setWeeklySchedule] = useState<WeeklyScheduleDto>(() =>
        Object.fromEntries(daysOfWeek.map(day => [day, { ...defaultTimeSlot }])) as WeeklyScheduleDto
    );

    const handleTimeChange = (
        day: keyof WeeklyScheduleDto,
        field: keyof TimeSlotDto,
        value: string
    ) => {
        setWeeklySchedule(prev => ({
            ...prev,
            [day]: {
                ...prev[day],
                [field]: value
            }
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const payload: PatchOpenRulesForRoomRequest = {
            roomId,
            openRules: {
                defaultOpenDate: new Date(),
                defaultCloseDate: new Date(),
                openTimesSingleDays: [],
                defaultOpenTimesForWeek: weeklySchedule
            }
        };

        try {
            await AdminApiCalls.patchOpenRulesForRoom(payload);
            alert("Room open rules updated!");
        } catch (error) {
            console.error(error);
            alert("Failed to patch open rules.");
        }
    };

    return (
        <div className="container">
            <form onSubmit={handleSubmit} className="reservation-form">
                <h2>Set Weekly Open Hours</h2>
                <div className="form-group">
                    <label htmlFor="roomId">Room ID</label>
                    <input
                        type="text"
                        id="roomId"
                        name="roomId"
                        value={roomId}
                        onChange={e => setRoomId(e.target.value)}
                        placeholder="Room ID"
                        required
                    />
                </div>

                {daysOfWeek.map(day => (
                    <div className="form-group" key={day}>
                        <label>{day.charAt(0).toUpperCase() + day.slice(1)}</label>
                        <div style={{ display: "flex", gap: "10px", flexWrap: "wrap" }}>
                            <input
                                type="time"
                                value={weeklySchedule[day].startTime.slice(0, 5)}
                                onChange={e => handleTimeChange(day, "startTime", e.target.value + ":00")}
                                required
                            />
                            <input
                                type="time"
                                value={weeklySchedule[day].endTime.slice(0, 5)}
                                onChange={e => handleTimeChange(day, "endTime", e.target.value + ":00")}
                                required
                            />
                        </div>
                    </div>
                ))}

                <button type="submit" className="submit-button">Save Weekly Schedule</button>
            </form>
        </div>
    );
};

export default PatchOpenRulesForm;
