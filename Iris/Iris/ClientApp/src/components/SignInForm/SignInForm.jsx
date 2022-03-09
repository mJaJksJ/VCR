import React, {useContext, useState} from 'react';
import IrisInput from "../UiComponents/IrisInput/IrisInput";
import IrisButton from "../UiComponents/IrisButton/IrisButton";
import styleClasses from './SignInForm.module.css';
import irisInputStyles from "../UiComponents/IrisInput/IrisInput.module.css"
import {AuthContext} from "../../context/AuthContext";
import Paths from "../../Paths";

const SignInForm = React.forwardRef((props, ref) => {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const {setIsAuth, user, setUser, setIsClickLogin} = useContext(AuthContext);

    const SignInRequest = async () => {
        const response = await fetch(Paths.auth, {
            method: 'POST',
        });

        if (response.ok) {
            const requestId = await response.text();
            const redirectUrl = Paths.auth+"/"+requestId;

            await SignInResult(redirectUrl);
        }
    }

    const SignInResult = async (redirectUrl) => {
        const userData = {
            login: login,
            password: password
        };

        const response = await fetch(redirectUrl, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(userData)
        });

        if (response.ok) {
            setIsAuth(true);
            setIsClickLogin(false);
            const result = await response.json();
            localStorage.setItem('token', result.token);
            localStorage.setItem('login', result.login);
            setUser({...user, Login: result.login})
        }
    }

    return (<div className={styleClasses.signInForm}>
        <div className={styleClasses.central}>
            <IrisInput
                value={login}
                onChange={e => setLogin(e.target.value)}
                type="text"
                placeholder="Логин"
                className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
            />
        </div>
        <div className={styleClasses.central}>
            <IrisInput
                value={password}
                onChange={e => setPassword(e.target.value)}
                type="password"
                placeholder="Пароль"
                className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
            />
        </div>
        <div className={styleClasses.central}>
            <IrisButton onClick={SignInRequest}>
                Войти
            </IrisButton>
        </div>
    </div>);
});

export default SignInForm;