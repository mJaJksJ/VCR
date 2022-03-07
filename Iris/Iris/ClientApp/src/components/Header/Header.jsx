import React, {useContext} from 'react';
import styleClasses from './Header.module.css';
import HeaderButton from "./HeaderButton/HeaderButton";
import {AuthContext} from "../../context/AuthContext";

const Header = React.forwardRef((props, ref) => {
    const {isAuth, setIsAuth, setIsClickLogin, user} = useContext(AuthContext);

    const exit = () => {
        setIsAuth(false);
    }

    let buttonPanel = !isAuth
        ? <HeaderButton onClick={() => {setIsClickLogin(true)}}>Войти</HeaderButton>
        : <>
            <HeaderButton onClick={exit}>Выход</HeaderButton>
            <HeaderButton>{user.Login}</HeaderButton>
        </>
    return (
        <div className={styleClasses.header} {...props}>
            {buttonPanel}
        </div>
    );
});

export default Header;