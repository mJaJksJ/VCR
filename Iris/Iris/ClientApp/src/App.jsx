import React, {useState} from 'react';
import './irisStyles.css'
import SignInForm from "./components/SignInForm/SignInForm";
import {AuthContext} from "./context/AuthContext";

const App = () => {
    const [isAuth, setIsAuth] = useState(false);

    return (
        <AuthContext.Provider value={{
            isAuth,
            setIsAuth,
        }}>
            <SignInForm/>
        </AuthContext.Provider>
    );
}
export default App;
