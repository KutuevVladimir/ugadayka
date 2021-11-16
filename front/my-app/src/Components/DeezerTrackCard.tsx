import {DeezerTrackDescription, RoomDescription, TrackDescription} from "../Api/Api";
import React from "react";
import {Form, ListGroup} from "react-bootstrap";

export function DeezerTrackCard(props: { track: DeezerTrackDescription, handleChange: (id: number)=>void}) {
    const Label = props.track.title + " : " +  props.track.artist.name;
    const Id = "" + props.track.id;
    return <Form><div key={props.track.id} className="mb-3">
        <Form.Check
            type='checkbox'
            id={Id}
            label={Label}
            onChange={()=>props.handleChange(props.track.id)}
        />
    </div>
    </Form>
}