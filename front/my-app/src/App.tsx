import React, {useState} from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route
} from "react-router-dom";
import {RoomsBrowser} from "./Components/RoomsBrowser";
import {RoomComponent} from "./Components/RoomComponent";
import Login from './Components/Login';
import {Redirect} from "react-router-dom";
import {TrackBrowser} from "./Components/TrackBrowser";
import {AddRoom} from "./Components/AddRoom";
import {AddingTrackBrowser} from "./Components/AddingTrackBrowser";
import { Homepage } from './Components/Homepage';

function App() {
    const [userId, setUserId] = useState<string|null>(localStorage.getItem("user_id"));

    let onLogout = () => {
        setUserId(null);
        localStorage.removeItem("user_id");
    };

    console.log("logged:" + !!userId);
    console.log("user_id:" + userId);

    if (userId == null)
        return (<Login onLogin={userId => {
            setUserId(userId);
            localStorage.setItem("user_id", userId);
        }} />);

    return (
        <Router>
            <Switch>
                <Route path="/tracks"><TrackBrowser userId={userId} onLogout={onLogout}/></Route>
                <Route path="/addtracks"><AddingTrackBrowser onLogout={onLogout}/></Route>
                <Route path="/addroom"><AddRoom userId={userId} onLogout={onLogout}/></Route>
                <Route path={"/:id"}><RoomComponent/></Route>
                <Route path="/"><Homepage onLogout={onLogout}/></Route>
                <Route> <Redirect to="/"/> </Route>
            </Switch>
        </Router>
    );
}

export default App;
