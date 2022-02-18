import React from 'react';
import styleClasses from './IrisButton.module.css';

const IrisButton = ({children, ...props}) => {
    return (
        <button className={styleClasses.irisButton} {...props}>
            {children}
        </button>
    );
};

export default IrisButton;
