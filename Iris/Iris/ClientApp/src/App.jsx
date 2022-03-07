import React, {useState} from 'react';
import './irisStyles.css'
import {AuthContext} from "./context/AuthContext";
import Header from "./components/Header/Header";
import Body from "./components/Body/Body";

const App = () => {
    const [isAuth, setIsAuth] = useState(false);
    const [isClickLogin, setIsClickLogin] = useState(false);

    return (
        <AuthContext.Provider value={{
            isAuth,
            setIsAuth,
            isClickLogin,
            setIsClickLogin
        }}>
            <Header />
            <Body />
        </AuthContext.Provider>
    );
}
export default App;
