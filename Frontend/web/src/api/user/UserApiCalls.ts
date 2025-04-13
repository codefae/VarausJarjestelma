
import {PostReservationRequest} from "../../models/postReservationRequest.ts";
import {GetRoomInfoResponse} from "../../models/getRoomInfoResponse.ts";
import {GetAvailableRoomsResponse} from "../../models/getAvailableRoomsResponse.ts";

const BASE_URL = 'http://192.168.159.23:5121/user';
const ROOM_BASE_URL = BASE_URL + '/rooms';
const RESERVATION_BASE_URL = BASE_URL + '/reservations';

export const UserApiCalls = {
    postReservation: async (postReservationRequest: PostReservationRequest) => {
        try {
            console.log(postReservationRequest)
            const response = await fetch(`${RESERVATION_BASE_URL}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(postReservationRequest),
            });
            if (!response.ok) {
                throw new Error(`Failed to post reservation: ${response.statusText}`);
            }
            return await response.json();
        } catch (error) {
            console.error('Error posting reservation:', error);
            throw error;
        }
    },

    getAvailableRooms: async (): Promise<GetAvailableRoomsResponse> =>{
      try {
          const response =  await fetch(`${ROOM_BASE_URL}`);
          if (!response.ok) {
              throw new Error('Failed to fetch room information');
          }

          // Parse the JSON directly from the response
          return  await response.json();
      } catch (error) {
          console.error('Error fetching available room');
          throw error;
      }
    },

    getRoomInfo: async (roomId: string): Promise<GetRoomInfoResponse> => {
        try {
            const response = await fetch(`${ROOM_BASE_URL}/${roomId}/info`);
            if (!response.ok) {
                throw new Error('Failed to fetch room information');
            }

            // Parse the JSON directly from the response

            return await response.json();;
        } catch (error) {
            console.error(`Error fetching room info for ID ${roomId}:`, error);
            throw error;
        }
    },


    // Delete a reservation by ID
    deleteReservation: async (reservationId: string) => {
        try {
            const response = await fetch(`${RESERVATION_BASE_URL}/${reservationId}`, {
                method: 'DELETE',
            });
            if (!response.ok) {
                throw new Error('Failed to delete reservation');
            }
        } catch (error) {
            console.error(`Error deleting reservation with ID ${reservationId}:`, error);
            throw error;
        }
    },
};