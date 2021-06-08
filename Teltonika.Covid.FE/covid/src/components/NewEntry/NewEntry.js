import React, {useState, useEffect} from 'react';
import "./NewEntry.scss";
import {createCase, fetchMetadata} from "../Services";

const defaultState = {
    metadata: null,
    errorMessage: null,
    gender: "",
    ageBracket: "",
    municipality: "",
    confirmationDate: "",
    Y: "",
    X: "",
};


const getSelectOptions = (options) => {
    return options.map((option, index) => {
        return (
            <option key={index} value={option.id}>{option.name}</option>
        );
    });
};

export default function NewEntry({token}) {
    const [state, setState] = useState(defaultState);

    useEffect(() => {
        if (!state.metadata)
            fetchMetadata()
                .then(response => response.json()
                    .then(data => {
                        if (!response.ok)
                            console.log(data.detail);
                        setState({...state, metadata: data});
                    })
                );
    });

    const handleCaseCreate = async (requestBody) => {
        return createCase(token, requestBody)
            .then(data => {
                if (data.status === 200 || data.status === 201) {
                    setState({...state, successMessage: "Created"});
                } else if (data.status === 401) {
                    setState({...state, errorMessage: "Unauthorized"});
                } else {
                    setState({...state, errorMessage: "Oops. Something went wrong :("});
                }
            });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setState({...state, errorMessage: null});
        handleCaseCreate({
            gender: state.gender,
            ageBracket: state.ageBracket,
            municipality: state.municipality,
            confirmationDate: state.confirmationDate,
            Y: state.Y,
            X: state.X,
        })
            .then(tokenResponse => {
                if (!!tokenResponse) {
                    setState(defaultState);
                }
            });
    };

    return (
        <div className="base-container">
            <div className="header">New entry</div>
            <div className="content">
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <select name="gender" value={state.gender}
                                onChange={e => setState({...state, gender: e.target.value})} required>
                            <option selected disabled hidden value="">Gender</option>
                            {!!state.metadata && getSelectOptions(state.metadata.genders)}
                        </select>
                        <select name="ageBracket" value={state.ageBracket}
                                onChange={e => setState({...state, ageBracket: e.target.value})} required>
                            <option selected disabled hidden value="">Age Brackets</option>
                            {!!state.metadata && getSelectOptions(state.metadata.ageBrackets)}
                        </select>
                        <select name="municipality" value={state.municipality}
                                onChange={e => setState({...state, municipality: e.target.value})} required>
                            <option selected disabled hidden value="">Municipality</option>
                            {!!state.metadata && getSelectOptions(state.metadata.municipalities)}
                        </select>
                        <input type="date" name="confirmationDate" value={state.confirmationDate}
                               onChange={e => setState({...state, confirmationDate: e.target.value})} required/>
                        <label>Y</label>
                        <input type="number" name="y" max="90" min="-90" maxLength="21" value={state.Y}
                               onChange={e => setState({...state, Y: e.target.value})} required/>
                        <label>X</label>
                        <input type="number" name="x" max="180" min="-180" maxLength="21" value={state.X}
                               onChange={e => setState({...state, X: e.target.value})} required/>
                    </div>
                    <div className="footer">
                        <button type="submit" className="btn">
                            Create
                        </button>
                    </div>
                </form>
                {state.errorMessage &&
                (<div className="error-container">
                    <label>{state.errorMessage}</label>
                </div>)}
                {state.successMessage &&
                (<div className="success-container">
                    <label>{state.successMessage}</label>
                </div>)}
            </div>
        </div>
    );
}

