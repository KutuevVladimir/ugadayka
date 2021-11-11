//GET:https://api.deezer.com/search?q=artist:"aloe blacc" track:"i need a dollar"

import React, {useEffect, useState} from "react";
import Api, {DeezerTrackDescription, PlayListDescription, TrackDescription} from "../Api/Api";
import {Button, ListGroup} from "react-bootstrap";
import {TrackCard} from "./TrackCard";
import {TrackState} from "./TrackBrowser"
import {Center, Input} from "@skbkontur/react-ui";
import SearchIcon from '@skbkontur/react-icons/Search';
import Logout from "./Logout";
import {DeezerTrackCard} from "./DeezerTrackCard";
import {Redirect} from "react-router-dom";

export function AddingTrackBrowser(props: { onLogout: () => void }){
    const [tracks, setTracks] = useState<DeezerTrackDescription[]>([]);
    const [tracksChecked, setTracksChecked] = useState<Set<number>>(new Set());
    const [tracksCheckedCount, setTracksCheckedCount] = useState(0);
    const [serverResponse, setServerResponse] = useState<boolean>(false);
    const [isSaved, setIsSaved] = useState<boolean>(false);
    const [searchString, setSearchString] = useState<string>("");

    useEffect(() => {
        if (serverResponse || !searchString)
            return;
        Api.getNewTracks(searchString).then(
            data => {
                setServerResponse(true);
                setTracks(data);
                data.forEach(value => console.log(value.id));
            });
    }, [searchString]);

   const checkboxChanged = (index: number)=> {
       if (tracksChecked.has(index)) {
           tracksChecked.delete(index);
           setTracksCheckedCount(tracksCheckedCount - 1);
       } else {
           tracksChecked.add(index);
           setTracksCheckedCount(tracksCheckedCount + 1);
       }
       setTracksChecked(tracksChecked);
   }

    const onClick = ()=> {
       let selected_tracks = tracks.filter(x => (tracksChecked.has(x.id)))
           .map(x => {return {id: x.id, name: x.title, author: x.artist.name, url: x.preview}
       });
       Api.addTracks(selected_tracks).then(r =>{setIsSaved(true);});
    }
    
    if(isSaved)
        return (<Redirect to={`/`}/>);
    
    return <div>
        <Input leftIcon={<SearchIcon />} onValueChange={value => {setServerResponse(false); setSearchString(value);}} />
        <ListGroup>
            {tracks.map(x => <ListGroup.Item key={x.id}> <DeezerTrackCard track={x} handleChange={checkboxChanged}/></ListGroup.Item>)}
        </ListGroup>
        <div className="d-grid gap-2">
            <Button disabled={!tracksCheckedCount} variant="primary" size="lg" onClick={onClick} >
                Add selected tracks
            </Button>
        </div>
    </div>
}