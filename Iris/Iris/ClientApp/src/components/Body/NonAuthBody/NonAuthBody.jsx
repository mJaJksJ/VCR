import React, {useContext} from 'react';
import {AuthContext} from "../../../context/AuthContext";
import SignInForm from "../../SignInForm/SignInForm";

const NonAuthBody = React.forwardRef(() => {
    const {isClickLogin} = useContext(AuthContext);

    return (
        <>
            {isClickLogin ? <SignInForm></SignInForm>: null}
        </>
    );
});

export default NonAuthBody;