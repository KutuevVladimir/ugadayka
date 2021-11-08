//GET:https://api.deezer.com/search?q=artist:"aloe blacc" track:"i need a dollar"

import React, {useEffect, useState} from "react";
import Api, {DeezerTrackDescription, PlayListDescription, TrackDescription} from "../Api/Api";
import {Button, ListGroup} from "react-bootstrap";
import {TrackCard} from "./TrackCard";
import {TrackState} from "./TrackBrowser"
import {Center} from "@skbkontur/react-ui";
import Logout from "./Logout";
import {DeezerTrackCard} from "./DeezerTrackCard";
import {Redirect} from "react-router-dom";

export function AddingTrackBrowser(props: { onLogout: () => void }){
    const [tracks, setTracks] = useState<DeezerTrackDescription[]>([]);
    const [tracksChecked, setTracksChecked] = useState<TrackState[]>([]);
    let [serverResponse, setServerResponse] = useState<boolean>(false);
    const [isSaved, setIsSaved] = useState<boolean>(false);

    useEffect(() => {
        if (serverResponse)
            return;
        Api.getNewTracks("lady gaga").then(
            data => {
                setServerResponse(true);
                setTracks(data);
                setTracksChecked(data.map(x => {
                    return new TrackState(x.id, false);
                }));
            });});

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
       let selected_tracks = tracks.filter(x => (tracksChecked.find(t => t.trackId === x.id) as TrackState).isChecked)
           .map(x => {return {id: x.id, name: x.title, author: x.artist.name, url: x.preview}
       });
       Api.addTracks(selected_tracks).then(r =>{setIsSaved(true);});
    }
    
    if(isSaved)
        return (<Redirect to={`/`}/>);
    
    if (tracks.length) {
        return <div>
            <ListGroup>
                {tracks.map(x => <ListGroup.Item key={x.id}> <DeezerTrackCard track={x} handleChange={checkboxChanged}/></ListGroup.Item>)}
            </ListGroup>
            <div className="d-grid gap-2">
                <Button variant="primary" size="lg" onClick={onClick}>
                    Add selected tracks
                </Button>

            </div>
        </div>
    }
    return (<Center><h3>Tracks not loaded :(</h3><Logout onLogout={props.onLogout}/></Center>);
}