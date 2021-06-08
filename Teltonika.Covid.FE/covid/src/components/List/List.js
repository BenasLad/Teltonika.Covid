import React from 'react';
import "./List.scss";
import {fetchCases, fetchMetadata} from "../Services";

const LIST_REQUEST_FIELDS = [
    "gender",
    "ageBracket",
    "municipality",
    "confirmationDateFrom",
    "confirmationDateTo",
    "pageSize",
    "page",
];

const formatDateString = (dateString) => {
    const date = new Date(dateString);
    return date.toISOString().slice(0, 10);
};

const getRow = (item, index) => {
    return (
        <tr key={index}>
            <td>{item.id}</td>
            <td>{item.gender}</td>
            <td>{item.age_bracket}</td>
            <td>{item.municipality}</td>
            <td>{formatDateString(item.confirmation_date)}</td>
            <td>{item.case_code}</td>
            <td>{item.X}</td>
            <td>{item.Y}</td>
        </tr>
    );
};

const getSelectOptions = (options, index) => {
    return options.map(option => {
        return (
            <option key={index} value={option.id}>{option.name}</option>
        );
    });
};

const fieldsChanged = (prevObj, currObj, fields) => {
    return !!fields.find(field => prevObj[field] !== currObj[field]);
};

const getMetadata = async () => {
    return fetchMetadata().then(response => {
            return response.json().then(data => {
                if (!response.ok)
                    console.log(data.detail);
                return (data);
            });
        });
};

const GetCases = async (listOptions) => {
    return fetchCases(listOptions)
        .then(response => {
            return response.json().then(data => {
                if (!response.ok)
                    console.log(data.detail);
                return (data);
            });
        });
};

const getListOptions = (state) => ({
    pageSize: state.pageSize,
    page: state.page,
    gender: state.gender,
    ageBracket: state.ageBracket,
    municipality: state.municipality,
    confirmationDateFrom: state.confirmationDateFrom,
    confirmationDateTo: state.confirmationDateTo,
});

export default class List extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            cases: false,
            metadata: false,
            gender: null,
            ageBracket: null,
            municipality: null,
            confirmationDateFrom: null,
            confirmationDateTo: null,
            pageSize: 10,
            page: 1,
            pageCount: 1
        };

        this.handleBasicChange = this.handleBasicChange.bind(this);
        this.handlePrevPageClick = this.handlePrevPageClick.bind(this);
        this.handleNextPageClick = this.handleNextPageClick.bind(this);

        Promise.all([getMetadata(), GetCases(getListOptions(this.state))]).then(results => {
            this.setState({
                metadata: results[0],
                cases: results[1].cases,
                page: results[1].page,
                pageCount: results[1].pageCount
            });
        });
    }

    handleBasicChange(e) {
        this.setState({[e.target.name]: e.target.value});
    }

    getHeaders() {
        if (!this.state.metadata)
            return (<></>);

        return (
            <thead>
            <tr>
                <th>Id</th>
                <th>
                    <select name="gender" value={this.state.gender} onChange={this.handleBasicChange}>
                        <option value={null}>Gender</option>
                        {getSelectOptions(this.state.metadata.genders)}
                    </select>
                </th>
                <th>
                    <select name="ageBracket" value={this.state.ageBracket} onChange={this.handleBasicChange}>
                        <option value={null}>
                            Age Brackets
                        </option>
                        {getSelectOptions(this.state.metadata.ageBrackets)}
                    </select>
                </th>
                <th>
                    <select name="municipality" value={this.state.municipality} onChange={this.handleBasicChange}>
                        <option value={null}>
                            Municipality
                        </option>
                        {getSelectOptions(this.state.metadata.municipalities)}
                    </select>
                </th>
                <th>
                    <input type="date" name="confirmationDateFrom"
                           onChange={this.handleBasicChange}/>
                    <input type="date" name="confirmationDateTo"
                           onChange={this.handleBasicChange}/>
                </th>
                <th>Case code</th>
                <th>X</th>
                <th>Y</th>
            </tr>
            </thead>
        );
    }

    handlePrevPageClick() {
        this.setState({
            page: this.state.page > 1 ? this.state.page - 1 : 1
        })
    }

    handleNextPageClick() {
        this.setState({
            page: this.state.page < this.state.pageCount ? this.state.page + 1 : this.state.pageCount
        })
    }


    componentDidUpdate(prevProps, prevState, snapshot) {
        if (fieldsChanged(prevState, this.state, LIST_REQUEST_FIELDS)) {
            const pageChanged = prevState.page !== this.state.page;


            return GetCases(getListOptions(this.state))
                .then(result => {
                    this.setState({cases: result.cases, page: pageChanged ? result.page : 1, pageCount: result.pageCount});
                });
        }
    }

    render() {
        let headers;
        if (!!this.state.metadata)
            headers = this.getHeaders();

        return (
            <div className="list-container">
                <div className="table-container">
                    <table>
                        {headers}
                        <tbody>
                        {!!this.state.cases && this.state.cases.map(getRow)}
                        </tbody>
                    </table>
                </div>
                <div className="paging-control-container">
                    <button onClick={() => this.setState({page: 1})}>{'<<'}</button>
                    <button onClick={this.handlePrevPageClick}>{'<'}</button>
                    <button onClick={this.handleNextPageClick}>{'>'}</button>
                    <button onClick={() => this.setState({page: this.state.pageCount})}>{'>>'}</button>
                    <span>Page <strong>{this.state.page} of {this.state.pageCount}</strong></span>
                    <select name="pageSize" value={this.state.pageSize} onChange={this.handleBasicChange}>
                        {[10, 20, 30, 40, 50].map((pageSize, index) => (
                            <option key={index} value={pageSize}>
                                Show {pageSize}
                            </option>
                        ))}
                    </select>
                </div>
            </div>
        );
    }
}