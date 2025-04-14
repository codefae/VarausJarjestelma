
import PostRoomForm from "../Components/PostRoomForm.tsx";
import AvailableRooms from "../Components/AvailableRooms.tsx";
import PostDeviceToRoomForm from "../Components/PostDeviceToRoomForm.tsx";

const Admin = () => {

    return (
        <div style={{ display: 'flex', justifyContent: 'space-between',flexWrap: 'wrap' }}>
            <div style={{ flex: 1 }}>
                <AvailableRooms isAdmin={true} />
            </div>
            <div style={{ flex: 1 }}>
                <PostRoomForm />
            </div>
            <div style={{ flex: 1 }}>
                <PostDeviceToRoomForm />
            </div>
        </div>
    );

}

export default Admin