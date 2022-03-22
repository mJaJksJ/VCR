import React, {useContext, useState} from 'react';
import IrisInput from "../UiComponents/IrisInput/IrisInput";
import IrisButton from "../UiComponents/IrisButton/IrisButton";
import styleClasses from './NewServerForm.module.css';
import irisInputStyles from "../UiComponents/IrisInput/IrisInput.module.css"
import IrisSelect from "../UiComponents/IrisSelect/IrisSelect";

const NewServerForm = React.forwardRef((props, ref) => {

    return (<div className={styleClasses.newServerForm}>
        <div className={styleClasses.title}>Добавить учетную запись</div>
        <div className={styleClasses.central}>
            <IrisSelect
                placeholder="Хост"
                className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
                options={["VK","Яндекс","Google","Outlook","Тест-сервер (localhost:3000)"]}
            />
        </div>
        <div className={styleClasses.central}>
            <IrisInput
                type="text"
                placeholder="Почтовый адрес"
                className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
            />
        </div>
        <div className={styleClasses.central}>
            <IrisSelect
                placeholder="Протокол"
                className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
                options={["Pop3","Imap"]}
            />
        </div>
        <div className={styleClasses.central}>
            <label className={`${styleClasses.central} ${irisInputStyles.irisInput} ${styleClasses.input}`}>
                <IrisInput
                    type="checkbox"
                    className={`${irisInputStyles.irisInput}`}
                />
                <label>Использовать SSL</label>
            </label>
        </div>
        <div className={styleClasses.central}>
            <IrisButton>
                Добавить
            </IrisButton>
        </div>
    </div>);
});

export default NewServerForm;