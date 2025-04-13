import { useState } from 'react';
import {PostReservationRequest} from "../models/postReservationRequest.ts";
import {UserApiCalls} from "../api/user/UserApiCalls.ts";
import './ReservationForm.css';

interface ReservationFormProps {
    roomId: string
}

const ReservationForm = ({roomId}: ReservationFormProps) => {
    const [formData, setFormData] = useState<PostReservationRequest>({
        userId: '49ba0ed2-e353-4cd3-a06b-e55d6bbe8c97',
        reservationDto: {
            roomId: roomId,
            reservationType: '',
            timeSlotDto: {
                startTime: '',
                endTime: ''
            },
            day: new Date(),
            deviceId: ''
        }
    });

    // Handle form field changes
    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            reservationDto: {
                ...prevState.reservationDto,
                [name]: value
            }
        }));
    };

    // Handle date change
    const handleDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData(prevState => ({
            ...prevState,
            reservationDto: {
                ...prevState.reservationDto,
                day: new Date(e.target.value)
            }
        }));
    };

    // Handle time slot change
    const handleTimeSlotChange = (e: React.ChangeEvent<HTMLInputElement>, timeType: 'startTime' | 'endTime') => {
        setFormData(prevState => ({
            ...prevState,
            reservationDto: {
                ...prevState.reservationDto,
                timeSlotDto: {
                    ...prevState.reservationDto.timeSlotDto,
                    [timeType]: e.target.value
                }
            }
        }));
    };

    // Handle form submission
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        console.log('Form Data:', formData);
        // Here you can make the API call to submit the reservation request
        try {
            // Replace with actual API call
            UserApiCalls.postReservation(formData)
            console.log('Reservation successful:');
        } catch (error) {
            console.error('Reservation failed:', error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="reservation-form">
            <h2>Make a Reservation</h2>



            <div className="form-group">
                <label htmlFor="reservationType">Reservation Type</label>
                <select
                    id="reservationType"
                    name="reservationType"
                    value={formData.reservationDto.reservationType}
                    onChange={handleChange}
                    required
                >
                    <option value="RoomReservation">Room</option>
                    <option value="DeviceReservation">Device</option>
                </select>
            </div>

            <div className="form-group">
                <label htmlFor="reservationDate">Reservation Date</label>
                <input
                    type="date"
                    id="reservationDate"
                    value={formData.reservationDto.day.toISOString().split('T')[0]}
                    onChange={handleDateChange}
                    required
                />
            </div>

            <div className="form-group">
                <label htmlFor="startTime">Start Time</label>
                <input
                    type="time"
                    id="startTime"
                    value={formData.reservationDto.timeSlotDto.startTime}
                    onChange={(e) => handleTimeSlotChange(e, 'startTime')}
                    required
                />
            </div>

            <div className="form-group">
                <label htmlFor="endTime">End Time</label>
                <input
                    type="time"
                    id="endTime"
                    value={formData.reservationDto.timeSlotDto.endTime}
                    onChange={(e) => handleTimeSlotChange(e, 'endTime')}
                    required
                />
            </div>

            <div className="form-group">
                <label htmlFor="deviceId">Device ID (Optional)</label>
                <input
                    type="text"
                    id="deviceId"
                    name="deviceId"
                    value={formData.reservationDto.deviceId || ''}
                    onChange={handleChange}
                />
            </div>

            <button type="submit" className="submit-button">Submit Reservation</button>
        </form>
    );
};

export default ReservationForm;
