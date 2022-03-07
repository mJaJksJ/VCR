import React, {useState} from 'react';
import './irisStyles.css'
import {AuthContext} from "./context/AuthContext";
import Header from "./components/Header/Header";
import Body from "./components/Body/Body";
import Paths from "./Paths";

const App = () => {
    const [isAuth, setIsAuth] = useState(false);
    const [user, setUser] = useState({});
    const [isClickLogin, setIsClickLogin] = useState(false);

    const checkIsAuth = async () => {
        const response = await fetch(Paths.checkIsAuth, {
            method: 'GET',
            headers: {
                Accept: "application/json",
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        });

        if (response.ok) {
            const result = await response.text();
            setIsAuth(result === "true");
        }
    }

    checkIsAuth();
    return (
        <AuthContext.Provider value={{
            isAuth, setIsAuth,
            isClickLogin, setIsClickLogin,
            user, setUser
        }}>
            <Header />
            <Body />
        </AuthContext.Provider>
    );
}
export default App;
