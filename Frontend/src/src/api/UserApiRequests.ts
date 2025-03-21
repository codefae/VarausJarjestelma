import GetAvailableRoomsResponse from "./dtos/GetAvailableRoomsResponse";

const backendAddress = "http//localhost:8000/"

// export const deleteReservation =  async (id: number) => {
//     try {
//         const response = await fetch(backendAddress + id, {
//             method: "DELETE",
//             headers: {"Content-Type": "application/json"},
//             body: JSON.stringify(FormData)
//         })
//         if (!response.ok) {
//             console.log('ResponseStatus: ${response.status}')
//         }
//     } catch (error) {
//         console.error(error.message);
//     }
// }

export const GetAvailableRooms = async () =>{
    try {
        const response = await fetch('${backendAddress}AvailableRoom', {
            method: "GET",
            headers: {"Content-Type": "application/json"},
        })
        if(response.ok){
            var json = response.json()
            var getAvailableRoomsResponse = new GetAvailableRoomsResponse(json)
        }
    }
}