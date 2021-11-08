import {Button, FloatingLabel, ListGroup} from "react-bootstrap";
import Form from "react-bootstrap/Form";
import React, {useEffect, useState} from "react";
import Api, {PlayListDescription, TrackDescription} from "../Api/Api";
import {Center} from "@skbkontur/react-ui";
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
export function TrackBrowser(props: { onLogout: () => void }){

    const [tracks, setTracks] = useState<TrackDescription[]>([]);
    const [tracksChecked, setTracksChecked] = useState<TrackState[]>([]);
    const [isSaved, setIsSaved] = useState<boolean>(false);
    
    useEffect(() => {
        Api.getAllTracks().then(data => {setTracks(data);
        setTracksChecked(data.map(x => {return new TrackState(x.id,false);}))
        })}, []);
    
    const checkboxChanged = (index: number)=> {
        let newArr = tracksChecked.map(x => {
            if (x.trackId === index) {
                x.isChecked = !x.isChecked
            } 
            return x;
        });
        setTracksChecked(newArr);
    }
    
    const onClick = ()=> {        
        Api.createPlayList({
            PlaylistId: -1,
            Name : "emiliya TEST playlist",
            Image : "playlist image",
            PlayerId : 1 ,
            trackIds : tracksChecked.filter(x => x.isChecked).map(filtered=> filtered.trackId)
            }).then(resp =>{setIsSaved(true);});
    }
    
    if(isSaved)
        return (<Redirect to={`/`}/>);
    
    if (tracks.length) {
        return <div>
            <ListGroup>
                {tracks.map(x => <ListGroup.Item key={x.id}> <TrackCard track={x} handleChange={checkboxChanged}/></ListGroup.Item>)}
            </ListGroup>
            <div className="d-grid gap-2">
                <Button variant="primary" size="lg" onClick={onClick}>
                    Create new Playlist
                </Button>

            </div>
        </div>
    }    

    return (<Center><h3>Tracks not found :(</h3><Logout onLogout={props.onLogout}/></Center>);
}