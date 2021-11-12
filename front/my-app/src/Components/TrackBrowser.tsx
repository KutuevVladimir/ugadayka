import {Button, FloatingLabel, ListGroup} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import React, {useEffect, useState} from "react";
import Api, {PlayListDescription, TrackDescription} from "../Api/Api";
import {Center, Input} from "@skbkontur/react-ui";
import Logout from "./Logout";
import {TrackCard} from "./TrackCard";
import {Redirect} from "react-router-dom";

export class  TrackState {
    trackId: number;
    isChecked: boolean;
    
    constructor(trackId:number, isChecked:boolean) {
        this.trackId = trackId;
        this.isChecked = isChecked;
    };
}
export function TrackBrowser(props: { userId: string, onLogout: () => void }) {

    const [tracks, setTracks] = useState<TrackDescription[]>([]);
    const [checkedTracks, setCheckedTracks] = useState<Set<number>>(new Set());
    const [checkedTracksCount, setCheckedTracksCount] = useState(0);
    const [isSaved, setIsSaved] = useState<boolean>(false);
    const [playlistName, setPlaylistName] = useState<string>("");

    useEffect(() => {
        Api.getAllTracks().then(data => setTracks(data))
    }, []);

    const checkboxChanged = (index: number)=> {
        if (checkedTracks.has(index)) {
            checkedTracks.delete(index);
            setCheckedTracksCount(checkedTracksCount - 1);
        } else {
            checkedTracks.add(index);
            setCheckedTracksCount(checkedTracksCount + 1);
        }
        setCheckedTracks(checkedTracks);
    }

    const onClick = ()=> {
        if (!! playlistName) {
            Api.createPlayList({
                PlaylistId: -1,
                Name: playlistName,
                PlayerId: props.userId,
                trackIds: Array.from(checkedTracks)
            }).then(() => {
                setIsSaved(true);
            });
        }
    }

    if(isSaved)
        return (<Redirect to={`/`}/>);

    if (tracks.length) {
        return <div>
            Playlist name: 
            <Input value={playlistName} onValueChange={setPlaylistName} />
            <ListGroup>
                {tracks.map(x => <ListGroup.Item key={x.id}> <TrackCard track={x} handleChange={checkboxChanged}/></ListGroup.Item>)}
            </ListGroup>
            <div className="d-grid gap-2">
                <Button disabled={!checkedTracksCount || !playlistName} variant="primary" size="lg" onClick={onClick}>
                    Create new Playlist
                </Button>

            </div>
        </div>
    }

    return (<Center><h3>Tracks not found :(</h3><Logout onLogout={props.onLogout}/></Center>);
}