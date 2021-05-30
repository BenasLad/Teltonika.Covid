import React, {useState} from 'react';
import { useHistory, Redirect } from 'react-router-dom'
import "./Login.scss";
import useToken from "./useToken";

export default function Login({setToken}) {
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();
    const [errorMessage, setErrorMessage] = useState();
    const history = useHistory();
    const { getToken } = useToken();

    const loginUser = async (credentials) => {
        return fetch('https://localhost:44385/token', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        })
            .then(data => {
                return data.json().then(response => {
                    let result = null;
                    if (!data.ok)
                        setErrorMessage(response.detail);
                    else
                        result = response;

                    return result;
                });
            });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrorMessage(null);
        loginUser({
            username,
            password
        })
            .then(tokenResponse => {
                if (!!tokenResponse) {
                    setToken(tokenResponse.token);
                    history.push("/newEntry");
                }
            });
    };

    if (!!getToken())
        return (<Redirect to="/newEntry" />);

    return (
        <div className="base-container">
            <div className="header">Login</div>
            <div className="content">
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Username</label>
                        <input type="text" name="username" placeholder="username"
                               onChange={e => setUserName(e.target.value)}/>
                    </div>
                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" name="password" placeholder="password"
                               onChange={e => setPassword(e.target.value)}/>
                    </div>
                    <div className="footer">
                        <button type="submit" className="btn">
                            Login
                        </button>
                    </div>
                </form>
                { errorMessage && <div className="error-container">
                    <label>{errorMessage}</label>
                </div>}
            </div>
        </div>
    );
}

