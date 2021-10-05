import React from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route
} from "react-router-dom";
import {RoomsBrowser} from "./Components/RoomsBrowser";
import {RoomComponent} from "./Components/RoomComponent";

function App() {
    return (
        <Router>
            <Switch>
                <Route path={"/:id"}><RoomComponent/></Route>
                <Route path="/"><RoomsBrowser/></Route>
            </Switch>
        </Router>
    );
}

export default App;
