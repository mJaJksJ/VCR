import React, {useContext} from 'react';
import {AuthContext} from "../../context/AuthContext";
import NonAuthBody from "./NonAuthBody/NonAuthBody";
import AuthBody from "./AuthBody/AuthBody";

const Body = React.forwardRef(() => {
    const {isAuth} = useContext(AuthContext);

    return (
        <>
            {!isAuth ? <NonAuthBody></NonAuthBody> : <AuthBody></AuthBody>}
        </>
    );
});

export default Body;