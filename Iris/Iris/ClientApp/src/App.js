import React, {Component, useState} from 'react';

import './irisStyles.css'
import IrisInput from "./components/ui-components/IrisInput";

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <>
                <IrisInput
                    type="text"
                    placeholder="Введите текст"
                />
            </>
        );
    }
}
