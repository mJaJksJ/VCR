import styleClasses from './Letter.module.css';
import irisInputStyles from "../UiComponents/IrisInput/IrisInput.module.css"
import React from 'react';

const Letter = React.forwardRef((props, ref) => {
    return (<div className={styleClasses.letter}>
        <div className={styleClasses.title}>Отчет 3. ПЗ 3. Предпринимательская деятельность. ПИН-44 Мясников М.А.</div>
        <div>
            <label
                style={{color: "var(----color-brown)", marginLeft: "10px"}}
            >От кого:
            </label>
            <label
                className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
            >maksim.m00@mail.ru
            </label>
            <label
                className={`${irisInputStyles.irisInput} ${styleClasses.input} ${styleClasses.right}`}
            > 20.03.2022 15:39
            </label>
        </div>
        <hr/>
        <div>
            <label
                style={{color: "var(----color-brown)", marginLeft: "10px"}}
            >Кому:
            </label>
            <label
                className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
            >bof_de@inbox.ru
            </label>
        </div>
        <hr/>
        <div>
            <label
                style={{color: "var(----color-brown)", marginLeft: "10px"}}
            >Вложения:
            </label>
            <div>
                <a
                    className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
                    href="https://vk.com/mjajksj"
                >Отчет 3. ПЗ 3. Предпринимательская деятельность. ПИН-44 Мясников М.А.docx
                </a>
            </div>
        </div><hr/>
        <div>
            <label
                style={{color: "var(----color-brown)", marginLeft: "10px"}}
            >Письмо:
            </label>
            <div>
                <label
                    className={`${irisInputStyles.irisInput} ${styleClasses.input}`}
                >
                    <br/>
                    --<br/>
                    С уважением,<br/>
                    Максим Мясников, ПИН-44<br/>
                </label>
            </div>
        </div>

    </div>);
});

export default Letter;