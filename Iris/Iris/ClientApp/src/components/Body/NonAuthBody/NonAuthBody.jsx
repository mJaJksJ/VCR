import React, {useContext} from 'react';
import {AuthContext} from "../../../context/AuthContext";
import SignInForm from "../../SignInForm/SignInForm";
import Letter from "../../Letter/Letter";

const NonAuthBody = React.forwardRef(() => {
    const {isClickLogin} = useContext(AuthContext);

    return (
        <>
            {isClickLogin ? <SignInForm></SignInForm>: null}
            <Letter></Letter>
        </>
    );
});

export default NonAuthBody;