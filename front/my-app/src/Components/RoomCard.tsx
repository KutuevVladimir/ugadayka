import {Center, Gapped, Link} from '@skbkontur/react-ui';
import {RoomDescription} from "../Api/Api";
import React from "react";

export function RoomCard(props: { room: RoomDescription, onClick: () => void }) {
    const {requiresPassword, playersCount, maxPlayersCount, name} = props.room
    return <Center>
        <Gapped>
            {requiresPassword && <div>🔒</div>}
            <Link
                disabled={requiresPassword || playersCount === maxPlayersCount}
                onClick={_ => props.onClick()}>
                <h3>{name}</h3>
            </Link>
            <div>{playersCount}/{maxPlayersCount}</div>
        </Gapped>
    </Center>;
}
