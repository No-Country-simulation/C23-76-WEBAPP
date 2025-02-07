import React from 'react';
import '../survey_click/survey_click.css';

const SurveyClick = ({ title, questions, tiempo, onClose }) => {
    return (
        <>
            <section className='box_survey'>
                <div className='white__box__survey'>
                    
                    <div className='items__and__buttons_survey'>
                    <h1>{title}</h1>
                      <p>*marque una casilla</p>
                      
                        <ol className='created__surveys_1'>
                            {questions.length > 0 ? (
                                questions.map((question, index) => (
                                    <li key={index} className='item__survey_1'>
                                        <h3>* {question.title} *</h3>
                                        <ul>
                                            {question.options && question.options.length > 0 ? (
                                                question.options.map((option, optionIndex) => (
                                                    <li key={optionIndex}>{option.text} <input type="checkbox" /></li>
                                                ))
                                            ) : (
                                                <li>No hay opciones disponibles</li>
                                            )}
                                        </ul>
                                    </li>
                                ))
                            ) : (
                                <li className='item__survey_1'>No hay preguntas</li>
                            )}
                        </ol>
                        <p>{tiempo}</p>
                    <button onClick={onClose} className="btn-back">Volver</button>
                    </div>
                    
                </div>
            </section> 
        </>
    );
};

export default SurveyClick;
