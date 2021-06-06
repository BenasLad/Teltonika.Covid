import React from 'react';
import "./List.scss";

const LIST_REQUEST_FIELDS = [
    "gender",
    "ageBracket",
    "municipality",
    "confirmationDateFrom",
    "confirmationDateTo",
    "pageSize",
    "page",
];

const items = [
    {
        id: 1,
        gender: "Vyras",
        ageBracket: "50-60",
        municipalityName: "Vilnius",
        municipalityCode: "1",
        confirmationDate: "2020-11-11",
        caseCode: "bbfc706a889651dbc82f892d5427ce9887848b229bb2d78db2e9b40e64fce642"
    },
    {
        id: 2,
        gender: "Moteris",
        ageBracket: "40-50",
        municipalityName: "Vilnius",
        municipalityCode: "1",
        confirmationDate: "2019-11-11",
        caseCode: "bbfc706a889651dbc82f892d5427ce9887848b229bb2d78db2e9b40e64fce642"
    }
];

const formatDateString = (dateString) => {
    const date = new Date(dateString);
    return date.toISOString().slice(0, 10);
};

const getRow = (item) => {
    return (
        <tr>
            <td>{item.id}</td>
            <td>{item.gender}</td>
            <td>{item.age_bracket}</td>
            <td>{item.municipality}</td>
            <td>{formatDateString(item.confirmation_date)}</td>
            <td>{item.case_code}</td>
        </tr>
    );
};

const getSelectOptions = (options) => {
    return options.map(option => {
        return (
            <option value={option.id}>{option.name}</option>
        );
    });
};

const parseSelectValue = (value) => {
    const parsed = parseInt(value, 10);
    return isNaN(parsed) ? null : parsed;
};

const fieldsChanged = (prevObj, currObj, fields) => {
    return !!fields.find(field => prevObj[field] !== currObj[field]);
};

const fetchMetadata = async () => {
    return fetch('https://localhost:44385/metadata', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            return response.json().then(data => {
                if (!response.ok)
                    console.log(data.detail);
                return (data);
            });
        });
};

const fetchCases = async (listOptions) => {
    return fetch('https://localhost:44385/cases/list', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            pageSize: parseSelectValue(listOptions.pageSize),
            page: listOptions.page,
            filters: {
                gender: parseSelectValue(listOptions.gender),
                ageBracket: parseSelectValue(listOptions.ageBracket),
                municipality: parseSelectValue(listOptions.municipality),
                confirmationDateFrom: listOptions.confirmationDateFrom,
                confirmationDateTo: listOptions.confirmationDateTo,
            }
        })
    })
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
        console.log("constructor");
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

        Promise.all([fetchMetadata(), fetchCases(getListOptions(this.state))]).then(results => {
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
        console.log("update");
        if (fieldsChanged(prevState, this.state, LIST_REQUEST_FIELDS)) {
            const pageChanged = prevState.page !== this.state.page;


            return fetchCases(getListOptions(this.state))
                .then(result => {
                    this.setState({cases: result.cases, page: pageChanged ? result.page : 1, pageCount: result.pageCount});
                });
        }
    }

    render() {
        console.log("render");
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
                        {[10, 20, 30, 40, 50].map(pageSize => (
                            <option value={pageSize}>
                                Show {pageSize}
                            </option>
                        ))}
                    </select>
                </div>
            </div>
        );
    }
}