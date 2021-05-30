import React, {useState} from 'react';
import { useHistory } from 'react-router-dom'
import "./Login.scss";

export default function Login({setToken}) {
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();
    const [errorMessage, setErrorMessage] = useState();
    const history = useHistory();

    const loginUser = async (credentials) => {
        return fetch('https://localhost:44385/token', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        })
            .then(data => {
                if (!data.ok) {
                    data.json().then(e => {
                        setErrorMessage(e.detail);
                    });
                }
                return data.json()
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
                setToken(tokenResponse.token);
                history.push("/newEntry");
            })
            .catch(e => {
                console.log(e);
            });
    };

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

