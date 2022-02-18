import React, {useContext, useState} from 'react';
import IrisInput from "../UiComponents/IrisInput/IrisInput";
import IrisButton from "../UiComponents/IrisButton/IrisButton";
import styleClasses from './SignInForm.module.css';
import irisInputStyles from "../UiComponents/IrisInput/IrisInput.module.css"
import {AuthContext} from "../../context/AuthContext";

const SignInForm = React.forwardRef((props, ref) => {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const {isAuth, setIsAuth} = useContext(AuthContext);

    const SignIn = () => {
        setIsAuth(true);
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
            <IrisButton onClick={SignIn}>
                Войти
            </IrisButton>
        </div>
    </div>);
});

export default SignInForm;