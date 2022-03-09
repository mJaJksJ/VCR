import React from 'react';
import styleClasses from './HeaderButton.module.css';

const HeaderButton = ({children, ...props}) => {
    return (
        <div className={styleClasses.headerButton} {...props}>
            <p>
                {children}
            </p>
        </div>
    );
};

export default HeaderButton;