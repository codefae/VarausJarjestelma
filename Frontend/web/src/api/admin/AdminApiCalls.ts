import {PostRoomRequest} from "../../models/postRoomRequest.ts";
import {PostDeviceToRoomRequest} from "../../models/postDeviceToRoomRequest.ts";

const BASE_URL = 'http://localhost:5121/admin';
const ROOM_BASE_URL = BASE_URL + '/room'; // Admin API:n reittipolku, määritelty backendissä

// Lisää huon
export const AdminApiCalls= {
    async addRoom(postRoomRequest: PostRoomRequest) {
        const response = await fetch( ROOM_BASE_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(postRoomRequest),
        });

        if (!response.ok) {
            const errorData = await response.json();

            // Optionally, log the error data for debugging
            console.error('Error Response:', errorData.reason);
            console.error('Error Response:', errorData.detail);


            // Throw the error message
            throw new Error(errorData?.reason ?? "" + errorData.detail);
        }
    },




     async addDeviceToRoom(postDeviceToRoomRequest: PostDeviceToRoomRequest) {
        console.log(postDeviceToRoomRequest)
        const response = await fetch(`${ROOM_BASE_URL}/device`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(postDeviceToRoomRequest),
        });

         if (!response.ok) {
             const errorText = await response.text();
             let errorData: any = {};

             try {
                 errorData = errorText ? JSON.parse(errorText) : {};
             } catch (e) {
                 console.warn("Failed to parse error response as JSON:", e);
             }

             console.error("Error Response:", errorData.reason);
             console.error("Error Detail:", errorData.detail);

             throw new Error(
                 errorData?.reason && errorData?.detail
                     ? `${errorData.reason}: ${errorData.detail}`
                     : errorData?.reason || errorData?.detail || 'Unknown error occurred'
             );
         }


         return await response.json(); // or return void if nothing is returned

     }
}
