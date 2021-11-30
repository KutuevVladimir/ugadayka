import {Center, Gapped, Link} from '@skbkontur/react-ui';
import React, {useEffect, useState} from "react";
import Api, {PlayListDescription} from '../Api/Api';
import Logout from './Logout';
import {RoomsBrowser} from './RoomsBrowser';

export function Homepage(props: { onLogout: () => void }) {
    const [playlists, setPlaylists] = useState<PlayListDescription[]>([]);
    const [userId, setUserId] = useState<string|null>(localStorage.getItem("user_id"));
    const [imageUrl, setImageUrl] = useState<string|null>("")

    useEffect(() => {
        Api.getAllPlayLists().then(data => setPlaylists(data))
        Api.getPlayerProfile(userId!).then(data => setImageUrl(data.image))
    }, []);

    return <Center>
        <Gapped gap={120}>
            <div><img src={imageUrl!}/></div>
            <Gapped gap={12} wrap style={{float: "left"}}>
                <div id={"Playlists"}>
                    {playlists.map(x => <Link
                        onClick={_ => {
                        }}>
                        <h3>{x.name}</h3>
                    </Link>)}
                </div>
            </Gapped>
            <Gapped vertical gap={12} style={{float: "left"}}>
                <div><RoomsBrowser/></div>
                <div><Logout onLogout={props.onLogout}/></div>
            </Gapped>
        </Gapped>
    </Center>
}
