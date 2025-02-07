import React, { useState } from 'react';

function SurveyItem({ item, onInputChange, onRemoveItem }) {
  // Estado para manejar los inputs
  const [inputs, setInputs] = useState([item.text, '', '']); // Inicializamos con tres inputs vacíos

  // Función para manejar el cambio en los inputs
  const handleChange = (event, index) => {
    const newInputs = [...inputs];
    newInputs[index] = event.target.value;  // Actualiza el input en el índice correspondiente
    setInputs(newInputs);  // Actualiza el estado de los inputs
    onInputChange(item.id, newInputs); // Pasa los cambios hacia el padre si es necesario
  };

  // Función para agregar un nuevo input
  const handleAddInput = () => {
    setInputs([...inputs, '']); // Añade un nuevo input vacío
  };

  return (
    <li className="survey-item">
      <div>
        {inputs.map((inputValue, index) => (
          <input
            key={index}
            type="text"
            value={inputValue}
            onChange={(e) => handleChange(e, index)}  // Llama a la función para manejar el cambio
            placeholder={index === 0 ? "Haz una pregunta" : "Opción de respuesta"}  // Placeholder condicional
            className={index === 0 ? "input__title" : "option__input"} // Cambia la clase según el índice
          />
        ))}
      </div>

      <button className='add-input' onClick={handleAddInput}>+</button>  {/* Botón para agregar un input */}

      <button
        className="quit-item"
        onClick={() => onRemoveItem(item.id)}  // Llama a la función de eliminación
      >
        x
      </button>
    </li>
  );
}

export default SurveyItem;
