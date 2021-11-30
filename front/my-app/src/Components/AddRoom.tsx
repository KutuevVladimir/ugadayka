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
    const [selectedPlaylist, setSelectedPlaylist] = useState<number>(-1);
    const [tracks, setTracks] = useState<TrackDescription[]>([]);

    useEffect(() => {
        Api.getAllPlayLists().then(data => setPlaylists(data))
        Api.getAllTracks().then(data => setTracks(data))
    }, []);

    if (isSaved)
        return (<Redirect to={`/` + roomId}/>);

    const onClick = () => {
        Api.createRoom({
            id: -1,
            name: roomName,
            playersCount: 0,
            maxPlayersCount: maxPlayers,
            requiresPassword: false,
            playlist: selectedPlaylist

        }).then(() => {
            setIsSaved(true);
        });
    }

    const renderPlaylist = (playlist: PlayListDescription) => <div>
        {playlist.name}
        <ListGroup>
            {playlist.trackIds.map(trackId => {
                    let track = tracks.find(track => track.id == trackId);
                    return track 
                        ? <ListGroup.Item> {track.name} </ListGroup.Item>
                        : <ListGroup.Item> {trackId} </ListGroup.Item>
                }
            )
            }
        </ListGroup>
    </div>

    if (playlists.length) {
        return <div>
            Room name:
            <Input value={roomName} onValueChange={setRoomName}/>
            Max players:
            <Input value={maxPlayers.toString()} onValueChange={(maxPlayers) => setMaxPlayers(Number(maxPlayers))}/>
            Password:
            <Input value={password} onValueChange={setPassword}/>
            Playlist:
            <RadioGroup onValueChange={(value: any) => setSelectedPlaylist(value)} items={playlists.map<[number, JSX.Element]>(playlist => [playlist.playlistId, renderPlaylist(playlist)])}/>

            <div className="d-grid gap-2">
                <Button disabled={!roomName || !maxPlayers || selectedPlaylist == -1} variant="primary" size="lg"
                        onClick={onClick}>
                    Create new room
                </Button>
            </div>
        </div>;
    }

    return (<Center><h3>Playlists not found :(</h3><Logout onLogout={props.onLogout}/></Center>);
}