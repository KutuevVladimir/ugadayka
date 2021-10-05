import React, {useEffect, useState} from "react";
import Api, {RoomDescription} from "../Api/Api";
import {Redirect} from "react-router-dom";
import {Center, Gapped} from "@skbkontur/react-ui";
import {RoomCard} from "./RoomCard";

export function RoomsBrowser() {
    const [rooms, setRooms] = useState<RoomDescription[]>([]);
    const [selectedRoom, selectRoom] = useState(-1);

    useEffect(() => {Api.getAll().then(data => setRooms(data))}, [])

    return selectedRoom !== -1
        ? <Redirect to={`/${selectedRoom}`}/>
        : (<Center>
            <Gapped gap={-1} wrap vertical>
                {rooms.map(x => <RoomCard room={x} onClick={() => selectRoom(x.id)}/>)}
            </Gapped>
        </Center>);
}
