import React, { useState } from 'react';
import icon from "../assets/icon.png";
import "./welcome.css";

const CardSurvey = ( { number} ) => {
    const [selectedOptions, setSelectedOptions] = useState([]);

    const handleOptionChange = (option) => {
        if (selectedOptions.includes(option)) {
        setSelectedOptions(selectedOptions.filter((o) => o !== option));
        } else {
        setSelectedOptions([...selectedOptions, option]);
        }
    };
    return (
            <div className="survey-container">
            <h2>Encuesta {number}</h2>
            <p>¿Quién puede participar en esta encuesta?</p>
            <div className="options-container">
                <div className='op-bd border'>
                    <div className='cnt-label'>
                        <img src={icon} />
                        <label htmlFor="todos">Todos</label>
                    </div>
                    <input
                        type="checkbox"
                        id="todos"
                        value="Todos"
                        checked={selectedOptions.includes('Todos')}
                        onChange={() => handleOptionChange('Todos')}
                    />
                </div>
                <div className='op-bd border'>
                    <div className='cnt-label'>
                        <img src={icon} />
                        <label htmlFor="url">URL</label>
                    </div>
                    <input
                        type="checkbox"
                        id="url"
                        value="URL"
                        checked={selectedOptions.includes('URL')}
                        onChange={() => handleOptionChange('URL')}
                    />
                </div>
                <div className='op-bd'>
                    <div className='cnt-label'>
                        <img src={icon} />
                        <label htmlFor="email">Invitados por Email</label>
                    </div>
                    
                    <input
                        type="checkbox"
                        id="email"
                        value="Invitados por Email"
                        checked={selectedOptions.includes('Invitados por Email')}
                        onChange={() => handleOptionChange('Invitados por Email')}
                    />
                </div>
            </div>
        </div>
    )
}

export default CardSurvey;