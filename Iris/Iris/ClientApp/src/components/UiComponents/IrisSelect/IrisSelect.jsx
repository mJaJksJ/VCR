import React from 'react';
import styleClasses from './IrisSelect.module.css';

const IrisInput = React.forwardRef((props, ref) => {
    let optionsList;
    for(let option in props.options){
        optionsList += <option>{option}</option>;
    }

    return (
        <select ref={ref} className={styleClasses.irisSelect} {...props}>
            {props.options.map(option => (<option className={styleClasses.irisOption}>{option}</option>))}
        </select>
    );
});

export default IrisInput;
