import React, {useContext} from 'react';
import styleClasses from './Header.module.css';
import HeaderButton from "./HeaderButton/HeaderButton";
import {AuthContext} from "../../context/AuthContext";
import Paths from "../../Paths";

const Header = React.forwardRef((props, ref) => {
    const {isAuth, setIsAuth, setIsClickLogin} = useContext(AuthContext);

    const exit = async () => {
        const response = await fetch(Paths.deauth, {
            method: 'POST',
            headers: {
                Accept: "application/json",
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        });

        if (response.ok) {
            setIsAuth(false);
        }
    }

    let buttonPanel = !isAuth
        ? <HeaderButton onClick={() => {setIsClickLogin(true)}}>Войти</HeaderButton>
        : <>
            <HeaderButton onClick={exit}>Выход</HeaderButton>
            <HeaderButton>{localStorage.getItem('login')}</HeaderButton>
        </>
    return (
        <div className={styleClasses.header} {...props}>
            {buttonPanel}
        </div>
    );
});

export default Header;