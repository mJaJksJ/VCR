import React from 'react';
import styleClasses from './MailsTable.module.css';
import IrisButton from "../UiComponents/IrisButton/IrisButton";
import IrisSelect from "../UiComponents/IrisSelect/IrisSelect";

const MailsTable = React.forwardRef((props, ref) => {

    return (<div className={styleClasses.mailsTable}>

        <table style={{textAlign: "left"}}>
            <thead>
            <tr className={styleClasses.title}>
                <th style={{width: "3vw", paddingLeft: "5px"}}></th>
                <th style={{width: "9vw", paddingLeft: "5px"}}>Получатель</th>
                <th style={{width: "9vw", paddingLeft: "5px"}}>Статус</th>
                <th style={{width: "9vw", paddingLeft: "5px"}}>Отправитель</th>
                <th style={{width: "45vw", paddingLeft: "5px"}}>Письмо</th>
                <th style={{width: "9vw", paddingLeft: "5px"}}>Вложения</th>
                <th style={{width: "9vw", paddingLeft: "5px"}}>Дата и время</th>
            </tr>
            </thead>
            <tbody>
            <tr>
                <td style={{textAlign: "center"}}><input type={"checkbox"}/></td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>mJaJksJ@mail.ru
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>Прочитано
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>Sunlight
                    </div>
                </td>
                <td style={{width: "45vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}> 🔥Специальное предложение на часы GUESS⌚️ Добавьте немного шика к вашему образу вместе с
                        эксклюзивными часами от известной марки GUESS с дополнительной скидкой -20% по промокоду 20GUESS
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>21.03.2022 17:34
                    </div>
                </td>
            </tr>
            <tr>
                <td style={{textAlign: "center"}}><input type={"checkbox"}/></td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>maksim.m00@mail.ru
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>Не прочитано
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>orioks@miet.ru
                    </div>
                </td>
                <td style={{width: "45vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}> ОРИОКС - Задание по Лабораторной работе 4
                        Уважаемый пользователь системы ОРИОКС!
                        Дисциплина: Нейронные сети
                        Автор объявления: Рычагов М.Н.
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>21.03.2022 14:52
                    </div>
                </td>
            </tr>
            <tr>
                <td style={{textAlign: "center"}}><input type={"checkbox"}/></td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>bof_de@inbox.ru
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>Черновик
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>maksim.m00@mail.ru
                    </div>
                </td>
                <td style={{width: "45vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}> Отчет 3. ПЗ 3. Предпринимательская деятельность. ПИН-44 Мясников М.А.
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>Отчет 3. ПЗ 3. Предпринимательская деятельность. ПИН-44 Мясников М.А.docx
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>20.03.2022 15:39
                    </div>
                </td>
            </tr>
            <tr>
                <td style={{textAlign: "center"}}><input type={"checkbox"}/></td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>bof_de@inbox.ru
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>Отправлено
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>maksim.m00@mail.ru
                    </div>
                </td>
                <td style={{width: "45vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}> Отчет 2. ПЗ 2. Рынок и рыночное регулирование экономики. ПИН-44 Мясников М.А.
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>
                        Отчет 2. ПЗ 2. Рынок и рыночное регулирование экономики. ПИН-44 Мясников М.А.
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>20.03.2022 15:35
                    </div>
                </td>
            </tr>
            <tr>
                <td style={{textAlign: "center"}}><input type={"checkbox"}/></td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>maksim.m00@mail.ru
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>Прочитано
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>Тинькофф Банк
                    </div>
                </td>
                <td style={{width: "45vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}> Как подружить ребенка с деньгами
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>
                    </div>
                </td>
                <td style={{width: "9vw"}}>
                    <div style={{
                        width: "inherit",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        textOverflow: "ellipsis"
                    }}>20.03.2022 11:50
                    </div>
                </td>
            </tr>
            </tbody>
        </table>
        <div>
            <IrisButton style={{width: "50px"}}>&lt;&lt;</IrisButton>
            <IrisButton disabled style={{width: "50px"}}>1</IrisButton>
            <IrisSelect options={[5,10,15,25]} style={{width: "50px"}}></IrisSelect>
            <IrisButton style={{width: "50px"}}>&gt;&gt;</IrisButton>
        </div>
    </div>);
});

export default MailsTable;