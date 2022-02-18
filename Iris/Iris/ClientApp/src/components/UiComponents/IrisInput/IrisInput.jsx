import React from 'react';
import styleClasses from './IrisInput.module.css';

const IrisInput = React.forwardRef((props, ref) => {
    return (
        <input ref={ref} className={styleClasses.irisInput} {...props}/>
    );
});

export default IrisInput;
