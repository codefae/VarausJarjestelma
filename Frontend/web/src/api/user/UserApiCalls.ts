
const BASE_URL = '/users'; 
const ROOM_BASE_URL = '/rooms';
const RESERVATION_BASE_URL = '/reservations';

export const UserApiCalls = {
    postReservation: async (reservationData: { userId: string; roomId: string; startTime: string; endTime: string }) => {
        try {
            const response = await fetch(`${RESERVATION_BASE_URL}/post`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(reservationData),
            });
            if (!response.ok) {
                throw new Error('Failed to post reservation');
            }
            return await response.json();
        } catch (error) {
            console.error('Error posting reservation:', error);
            throw error;
        }
    },


    getRoomInfo: async (roomId: string) => {
        try {
            const response = await fetch(`${ROOM_BASE_URL}/info/${roomId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch room information');
            }
            return await response.json();
        } catch (error) {
            console.error(`Error fetching room info for ID ${roomId}:`, error);
            throw error;
        }
    },

    // Delete a reservation by ID
    deleteReservation: async (reservationId: string) => {
        try {
            const response = await fetch(`${RESERVATION_BASE_URL}/delete/${reservationId}`, {
                method: 'DELETE',
            });
            if (!response.ok) {
                throw new Error('Failed to delete reservation');
            }
            return await response.json();
        } catch (error) {
            console.error(`Error deleting reservation with ID ${reservationId}:`, error);
            throw error;
        }
    },
};