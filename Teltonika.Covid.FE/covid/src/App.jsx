import React, {useState} from 'react';
import { BrowserRouter, Route, Switch, Link } from 'react-router-dom';

import LoginForm from "./components/Login/Login";
import useToken from './components/Login/useToken';
import Home from "./components/Home/Home";
import NewEntry from "./components/NewEntry/NewEntry";
import List from "./components/List/List";


function App() {
    const [token, setTokenToState] = useState();
    const { getToken, setToken } = useToken();
    const persistedToken = getToken();
    if (persistedToken !== token)
        setTokenToState(persistedToken);

    const handleTokenSet = (tokenToSet) => {
        setToken(tokenToSet);
        setTokenToState(tokenToSet);
    };

    return (
        <div className="App">
            <BrowserRouter>
                <nav>
                    <ul>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/list">List</Link></li>
                        { token && <li><Link to="/newEntry">New Entry</Link></li> }
                        { !token && <li><Link to="/login">Login</Link></li> }
                    </ul>
                </nav>
                <Switch>
                    <Route exact path="/">
                        <Home />
                    </Route>
                    <Route path="/list">
                        <List />
                    </Route>
                    <Route path="/newEntry">
                        <NewEntry />
                    </Route>
                    <Route path="/login">
                        <LoginForm setToken={handleTokenSet}/>
                    </Route>
                </Switch>
            </BrowserRouter>
        </div>
    );
}

export default App;