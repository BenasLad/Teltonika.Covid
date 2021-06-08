
export default function useToken() {
    const getToken = () => {
        const tokenString = sessionStorage.getItem('token');
        return JSON.parse(tokenString);
    };

    const saveToken = userToken => {
        sessionStorage.setItem('token', JSON.stringify(userToken));
    };

    return {
        setToken: saveToken,
        getToken: getToken
    }
}