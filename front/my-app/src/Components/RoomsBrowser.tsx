import React, {useEffect, useState} from "react";
import Api, {RoomDescription} from "../Api/Api";
import {Redirect} from "react-router-dom";
import {Center, Gapped} from "@skbkontur/react-ui";
import {RoomCard} from "./RoomCard";
import Logout from "./Logout";

export function RoomsBrowser(props: { onLogout: () => void }) {
    const [rooms, setRooms] = useState<RoomDescription[]>([]);
    const [selectedRoom, selectRoom] = useState(-1);

    useEffect(() => {Api.getAll().then(data => setRooms(data))}, [])

    if (selectedRoom !== -1)
        return (<Redirect to={`/${selectedRoom}`}/>);

    if (rooms.length)
        return (<Center>
                <Gapped gap={-1} wrap vertical>
                    {rooms.map(x => <RoomCard key={x.id} room={x} onClick={() => selectRoom(x.id)}/>)}
                    <Logout onLogout={props.onLogout}/>
                </Gapped>
                </Center>);

    return (<Center><h3>Rooms not found :(</h3><Logout onLogout={props.onLogout}/></Center>);
}
