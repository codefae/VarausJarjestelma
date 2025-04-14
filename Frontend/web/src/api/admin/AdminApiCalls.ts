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
            const errorText = await response.text();
            let errorData: any= {};

            try {
                errorData = errorText ? JSON.parse(errorText) : {};
            } catch (e) {
                console.warn("Failed to parse error response as JSON:", e);
            }

            let errorMessage = ''
            // Optionally, log the error data for debugging
            console.error('Error Response:', errorData.reason);
            console.error('Error Response:', errorData.detail);

            if (errorData?.errors) {


                const fieldErrors = Object.entries(errorData.errors)
                    .map(([key, messages]) => ` ${(messages as string[]).join(", ")}`)
                    .join("\n");

                errorMessage += fieldErrors;
                throw new Error(errorMessage + errorData)
            }

            console.error(errorData)

            // Throw the error message
            throw new Error(
                errorData?.reason && errorData?.detail
                    ? `${errorData.reason}: ${errorData.detail}`
                    : errorData?.reason || errorData?.detail || errorData|| 'Unknown error occurred'
            );
        }
    },




     async addDeviceToRoom(postDeviceToRoomRequest: PostDeviceToRoomRequest) {

         const response = await fetch(`${ROOM_BASE_URL}/device`, {
             method: "POST",
             headers: {
                 "Content-Type": "application/json",
             },
             body: JSON.stringify(postDeviceToRoomRequest),
         });

         if (!response.ok) {
             const errorText = await response.text();
             let errorData: any= {};

             try {
                 errorData = errorText ? JSON.parse(errorText) : {};
             } catch (e) {
                 console.warn("Failed to parse error response as JSON:", e);
             }
             let errorMessage: string = ''
             if (errorData?.errors) {
                 const fieldErrors = Object.entries(errorData.errors)
                     .map(([key, messages]) => ` ${(messages as string[]).join(", ")}`)
                     .join("\n");

                 errorMessage += fieldErrors;

                 console.error("Error Response:", errorData.reason);
                 console.error("Error Detail:", errorData.detail);
                 throw new Error(errorMessage)
             }
             throw new Error(
                 errorData?.reason && errorData?.detail
                     ? `${errorData.reason}: ${errorData.detail}`
                     : errorData?.reason || errorData?.detail || 'Unknown error occurred'
             );
         }


         return await response.json(); // or return void if nothing is returned

     },
    async deleteRoom(roomId: string){
        const response = await fetch(`${ROOM_BASE_URL}/${roomId}`, {
            method: "DELETE",
        });

        if (!response.ok) {



            // Throw the error message
            throw new Error("falied to delete");
        }
    },
    async deleteDeviceFromRoom(roomId: string, deviceId: string){
        const response = await fetch(`${ROOM_BASE_URL}/${roomId}/device/${deviceId}`, {
            method: "DELETE",
        });

        if (!response.ok) {



            // Throw the error message
            throw new Error("falied to delete");
        }
    }
}
