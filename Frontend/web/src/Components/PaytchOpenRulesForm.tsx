import { useState } from 'react';
import './ReservationForm.css';
import { AdminApiCalls } from '../api/admin/AdminApiCalls.ts';
import {OpenRulesDto} from "../models/openRulesDto.ts";
import {PatchOpenRulesForRoomRequest} from "../models/PatchOpenRulesForRoomRequest.ts";
import {WeeklyScheduleDto} from "../models/WeeklyScheduleDto.ts";


interface OpenRulesFormProps {
    roomId: string;
    initialOpenRules: OpenRulesDto;
    reservationAdded: () => void;
}

const OpenRulesForm = ({ roomId, initialOpenRules, reservationAdded }: OpenRulesFormProps) => {
    const [formData, setFormData] = useState<PatchOpenRulesForRoomRequest>({
        roomId,
        openRules: initialOpenRules,
    });

    const handleTimeSlotChange = (
        day: keyof WeeklyScheduleDto, // Ensure 'day' is a valid key of WeeklyScheduleDto
        field: 'startTime' | 'endTime',
        value: string
    ) => {
        setFormData(prev => ({
            ...prev,
            openRules: {
                ...prev.openRules,
                defaultOpenTimesForWeek: {
                    ...prev.openRules.defaultOpenTimesForWeek,
                    [day]: {
                        ...prev.openRules.defaultOpenTimesForWeek[day],
                        [field]: value,
                    },
                },
            },
        }));
    };


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await AdminApiCalls.patchOpenRulesForRoom(formData);
            alert('Open rules updated successfully!');
            reservationAdded();
        } catch (error) {
            console.error(error);
            alert(error);
        }
    };

    return (
        <div className="container">
            <form onSubmit={handleSubmit} className="reservation-form">
                <h2>Edit Weekly Open Rules</h2>

                {Object.entries(formData.openRules.defaultOpenTimesForWeek).map(([day, timeSlot]) => (
                    <div key={day} className="form-group">
                        <label>{day.charAt(0).toUpperCase() + day.slice(1)}</label>
                        <div className="time-inputs">
                            <input
                                type="time"
                                value={timeSlot.startTime}
                                onChange={e => handleTimeSlotChange(day as keyof WeeklyScheduleDto, 'startTime', e.target.value)} // Correctly cast to keyof WeeklyScheduleDto
                                required
                            />
                            <span>to</span>
                            <input
                                type="time"
                                value={timeSlot.endTime}
                                onChange={e => handleTimeSlotChange(day as keyof WeeklyScheduleDto, 'endTime', e.target.value)} // Correctly cast to keyof WeeklyScheduleDto
                                required
                            />
                        </div>
                    </div>
                ))}

                <button type="submit" className="submit-button">Save Open Rules</button>
            </form>
        </div>
    );
};

export default OpenRulesForm;
