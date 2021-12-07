import {Center, Gapped, Link} from '@skbkontur/react-ui';
import React, {useEffect, useState} from "react";
import Api, {PlayListDescription} from '../Api/Api';
import { AddRoom } from './AddRoom';
import Logout from './Logout';
import {RoomsBrowser} from './RoomsBrowser';
import {Redirect, useHistory} from "react-router-dom";

export function Homepage(props: { onLogout: () => void }) {
    const [playlists, setPlaylists] = useState<PlayListDescription[]>([]);
    const [userId, setUserId] = useState<string|null>(localStorage.getItem("user_id"));
    const [imageUrl, setImageUrl] = useState<string|null>("")
    const history = useHistory()

    useEffect(() => {
        Api.getAllPlayLists().then(data => setPlaylists(data))
        Api.getPlayerProfile(userId!).then(data => setImageUrl(data.image))
    }, []);

    return <Center>
        <Gapped gap={120}>
            <div><img src={imageUrl!}/></div>
            <Gapped vertical gap={12} style={{float: "left"}}>
                <div id={"Playlists"} >
                    {playlists.map(x => <Link
                        onClick={_ => {
                        }}>
                        <h3>{x.name}</h3>
                    </Link>)}
                </div>
                <div><button onClick={_ => history.push('/tracks')}>Add Playlist</button></div>
            </Gapped>
            <Gapped vertical gap={12} style={{float: "left"}}>
                <div><RoomsBrowser/></div>
                <div><button onClick={_ => history.push('/AddRoom')}>Add Room</button></div>
            </Gapped>
            <Gapped vertical gap={12} style={{float: "left"}}>
                <div><Logout onLogout={props.onLogout}/></div>
            </Gapped>             
        </Gapped>
    </Center>
}
