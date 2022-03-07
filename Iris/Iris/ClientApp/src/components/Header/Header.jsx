import React, {useContext} from 'react';
import styleClasses from './Header.module.css';
import HeaderButton from "./HeaderButton/HeaderButton";
import {AuthContext} from "../../context/AuthContext";

const Header = React.forwardRef((props, ref) => {
    const {isAuth, setIsClickLogin} = useContext(AuthContext);

    let buttonPanel = !isAuth
        ? <HeaderButton>Войти</HeaderButton>
        : <HeaderButton>User</HeaderButton>
    return (
        <div className={styleClasses.header} onClick={() => {setIsClickLogin(true)}} {...props}>
            {buttonPanel}
        </div>
    );
});

export default Header;