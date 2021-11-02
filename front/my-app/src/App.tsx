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
import {AddingTrackBrowser} from "./Components/AddingTrackBrowser";

function App() {
    let [logged, setLogged] = useState(false);

    if (!logged)
        return (<Login onLogin={() => setLogged(true)}/>);
    
    function createNewPlayList(){
        
    }
    return (
        <Router>
            <Switch>
                <Route path="/tracks"><TrackBrowser onLogout={() => setLogged(false)}/></Route>
                <Route path="/addtracks"><AddingTrackBrowser onLogout={() => setLogged(false)}/></Route>
                <Route path={"/:id"}><RoomComponent/></Route>
                <Route path="/"><RoomsBrowser onLogout={() => setLogged(false)}/></Route>                
                <Route> <Redirect to="/"/> </Route>
            </Switch>
        </Router>
    );
}

export default App;
