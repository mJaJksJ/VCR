import React from 'react';
import MailsTable from "../../MailsTable/MailsTable";
import NewServerForm from "../../NewServerForm/NewServerForm";

const AuthBody = React.forwardRef(() => {
    return (
        <>
            <MailsTable></MailsTable>
            <NewServerForm></NewServerForm>
        </>
    );
});

export default AuthBody;