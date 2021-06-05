import React from 'react';
import "./List.scss";

export default function List() {
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

    const getRow = (item) => {
        return (
            <tr>
                <td>{item.id}</td>
                <td>{item.gender}</td>
                <td>{item.ageBracket}</td>
                <td>{item.municipalityName} - {item.municipalityCode}</td>
                <td>{item.confirmationDate}</td>
                <td>{item.caseCode}</td>
            </tr>
        );
    };

    const metadata = {
        genders: [{ id: 1, name: "name" }],
        ageBrackets: [{ id: 1, name: "name" }],
        municipalities: [{ id: 1, name: "name" }]
    };

    const a = {
        pageSize: 10,
        page: 1,
        filters: {
            gender: null,
            ageBracket: null,
            municipality: null,
            confirmationDateFrom: null,
            confirmationDateTo: null,
        }
    };

    return (
        <div className="list-container">
            <div className="table-container">
            <table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Gender</th>
                        <th>Age bracket</th>
                        <th>Municipality</th>
                        <th>Confirmation date</th>
                        <th>Case code</th>
                    </tr>
                </thead>
                <tbody>
                    {items.map(getRow)}
                </tbody>
            </table>
            </div>
            <div className="paging-control-container">
                <button>{'<<'}</button>
                <button>{'<'}</button>
                <button>{'>'}</button>
                <button>{'>>'}</button>
                <span>Page <strong>{0} of {0}</strong></span>
                <select>
                    {[10, 20, 30, 40, 50].map(pageSize => (
                        <option key={pageSize} value={pageSize}>
                            Show {pageSize}
                        </option>
                    ))}
                </select>
            </div>
        </div>
    );
}