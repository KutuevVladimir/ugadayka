import {Button, FloatingLabel, ListGroup} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import React, {useEffect, useState} from "react";
import Api, {PlayListDescription, TrackDescription} from "../Api/Api";
import {Center, Input, RadioGroup} from "@skbkontur/react-ui";
import Logout from "./Logout";
import {RoomCard} from "./RoomCard";
import {Redirect} from "react-router-dom";

export function AddRoom(props: { userId: string, onLogout: () => void }) {
    const [playlists, setPlaylists] = useState<PlayListDescription[]>([]);
    const [isSaved, setIsSaved] = useState<boolean>(false);
    const [roomName, setRoomName] = useState<string>("");
    const [roomId, setRoomId] = useState<string>("");
    const [maxPlayers, setMaxPlayers] = useState<number>(0);
    const [password, setPassword] = useState<string>("");

    /*useEffect(() => {
        Api.getAllPlayLists().then(data => setPlaylists(data))
    }, []);*/
    
    /*if(isSaved)
        return (<Redirect to={`/`+roomId}/>);*/

    /*const onClick = ()=> {
            Api.createRoom({
                id: -1,
                name: roomName,
                playersCount: 0,
                maxPlayersCount: maxPlayers,
                requiresPassword: false,
                playlist: 2
                
            }).then(() => {
                setIsSaved(true);
            });
    }*/

    if (playlists.length) {
        return <div>1</div> /*<div>
            Room name:
            <Input value={roomName} onValueChange={setRoomName}/>
            Max players:
            <Input value={maxPlayers.toString()} onValueChange={(maxPlayers) => setMaxPlayers(Number(maxPlayers))}/>
            Password:
            <Input value={password} onValueChange={setPassword}/>
            Playlist:
            <RadioGroup items={playlists}/>

            <div className="d-grid gap-2">
                <Button disabled={!roomName || !maxPlayers} variant="primary" size="lg" onClick={()=>console.log("nope")}>
                    Create new room
                </Button>
            </div>
        </div>;*/
    }

    return (<Center><h3>Playlists not found :(</h3><Logout onLogout={props.onLogout}/></Center>);
}