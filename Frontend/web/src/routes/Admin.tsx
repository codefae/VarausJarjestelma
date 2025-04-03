import {useEffect, useState} from "react";
import {GetAvailableRoomsResponse} from "../models/getAvailableRoomsResponse.ts";
import {Form} from "react-router";
import PostRoomForm from "../Components/PostRoomForm.tsx";
import AvailableRooms from "../Components/AvailableRooms.tsx";

const Admin = () => {
    const [getRoomInfoResponses, setGetRoomInfoResponses] = useState<GetAvailableRoomsResponse>();
    useEffect(() => {
    })
  return <>
        <AvailableRooms></AvailableRooms>
        <PostRoomForm></PostRoomForm>

    </>
}

export default Admin