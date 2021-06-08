const API_SERVER = "https://localhost:44385";


const parseSelectValue = (value) => {
    const parsed = parseInt(value, 10);
    return isNaN(parsed) ? null : parsed;
};

const fetchToken = async (credentials) => {
    return fetch(`${API_SERVER}/token`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
    });
};

const fetchMetadata = async () => {
    return fetch(`${API_SERVER}/metadata`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
};

const createCase = async (token, caseFields) => {
    return fetch(`${API_SERVER}/cases`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({
            gender: parseSelectValue(caseFields.gender),
            ageBracket: parseSelectValue(caseFields.ageBracket),
            municipality: parseSelectValue(caseFields.municipality),
            confirmationDate: caseFields.confirmationDate,
            Y: caseFields.Y,
            X: caseFields.X,
        })
    });
};


const fetchCases = async (listOptions) => {
    return fetch(`${API_SERVER}/cases/list`, {
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
    });
};

export {
    createCase,
    fetchToken,
    fetchMetadata,
    fetchCases
};