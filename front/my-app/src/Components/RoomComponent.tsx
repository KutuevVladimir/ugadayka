import React, {useEffect, useState} from "react";
import {useHistory, useParams} from "react-router-dom";
import Api, {Room} from "../Api/Api";
import {Button, Center, Gapped} from "@skbkontur/react-ui";

export function RoomComponent() {
    let {id} = useParams<{ id: string }>();
    const [roomData, setRoomData] = useState<Room | null>(null);
    const history = useHistory()

    useEffect(() => 
        {
            Api.addPlayerToRoom({idRoom : Number(id), idPlayer: localStorage.getItem("user_id")!, is_remove: 0}).
                then(_ => Api.get(id).then(data => setRoomData(data)))
        }, 
        [id])

    return roomData &&
        <div>
        <Center style={{marginTop: "10px"}}>
            <Button onClick={_ => Api.addPlayerToRoom({idRoom : Number(id), idPlayer: localStorage.getItem("user_id")!, is_remove: 1}).
            then(_ => history.push("/"))}>К списку</Button>
        </Center>
        <Center style={{marginTop: "10px"}}>
            <h1>{roomData.description.name}</h1>
            <Gapped verticalAlign={"top"}>
                <div style={{borderStyle: "solid", textAlign: "center"}}>
                    <b style={{marginLeft: "100pt", marginRight: "100pt"}}>Игроки</b>
                    {roomData.players.map(x => <p>{x.nickName}</p>)}
                </div>
                <div style={{borderStyle: "solid", textAlign: "center"}}>
                    <Gapped verticalAlign={"top"} wrap vertical>
                    <b style={{marginLeft: "100pt", marginRight: "100pt"}}>Песни</b>
                    {roomData.songs.map(x => <audio key={x.url} controls style={{marginLeft: "10pt", marginRight: "10pt"}}> <source src={x.url}/> </audio> )}
                    </Gapped>
                </div>
            </Gapped>
        </Center>
        </div>;
}
