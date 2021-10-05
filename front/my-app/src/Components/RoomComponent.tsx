import React, {useEffect, useState} from "react";
import {useHistory, useParams} from "react-router-dom";
import Api, {Room} from "../Api/Api";
import {Button, Center, Gapped} from "@skbkontur/react-ui";

export function RoomComponent() {
    let {id} = useParams<{ id: string }>();
    const [roomData, setRoomData] = useState<Room | null>(null);
    const history = useHistory()

    useEffect(() => {Api.get(id).then(data => setRoomData(data))}, [])

    return roomData &&
        <div>
        <Center style={{marginTop: "10px"}}>
            <Button onClick={_ => history.push("/")}>К списку</Button>
        </Center>
        <Center style={{marginTop: "10px"}}>
            <h1>{roomData.description.name}</h1>
            <Gapped verticalAlign={"top"}>
                <table style={{borderStyle: "solid", textAlign: "center"}}>
                    <td><b>Игроки</b></td>
                    <tbody>
                    {roomData.players.map(x => <tr>{x.nickName}</tr>)}
                    </tbody>
                </table>
                <table style={{borderStyle: "solid", textAlign: "center"}}>
                    <td><b>Песни</b></td>
                    <tbody>
                    {roomData.songs.map(x => <tr>{x}</tr>)}
                    </tbody>
                </table>
            </Gapped>
        </Center>
        </div>;
}
