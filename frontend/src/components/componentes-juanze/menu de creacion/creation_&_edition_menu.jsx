import './creation_&_edition_menu.css';
import { useNavigate } from 'react-router-dom';
import SurveyItem from './survey_item';
import { useState } from 'react';

function CreateSurvey() {
  const navigate = useNavigate();
  const [title, setTitle] = useState('New Survey'); // Nombre de la encuesta
  const [items, setItems] = useState([]);
  const [visibility, setVisibility] = useState([]);
  const [timeLimit, setTimeLimit] = useState(0);
  const token =  localStorage.getItem("token"); //" // Reemplázalo con tu token

  
  const handleAddItem = () => {
    setItems([
      ...items,
      { id: items.length + 1, title: '', options: [] }
    ]);
  };

  const handleInputChange = (id, values) => {
    setItems(items.map(item =>
      item.id === id
        ? { ...item, title: values[0], options: values.slice(1).map(text => ({ text })) }
        : item
    ));
  };

  const handleRemoveItem = (id) => {
    setItems(items.filter(item => item.id !== id));
  };

  const handleVisibilityChange = (option) => {
    setVisibility(prev =>
      prev.includes(option) ? prev.filter(v => v !== option) : [...prev, option]
    );
  };

  const handleSubmit = async () => {
    const surveyData = {
      title: title,
      type: "TIME",
      expiresAt: timeLimit > 0 ? new Date(Date.now() + timeLimit * 60000).toISOString() : null,
      startsAt: new Date().toISOString(),
      allowAnonymousVotes: visibility.includes("Publico"),
      votesAmountRequiredToFinish: null,
      questions: items.map((item, index) => ({
        id: index + 1,
        title: item.title,
        type: "SingleChoice",
        maxSelections: 1,
        options: item.options.map((option, optIndex) => ({
          id: optIndex + 1,
          text: option.text
        }))
      }))
    };

    try {
      const response = await fetch('https://surveymaker-53d73b4bd329.herokuapp.com/survey', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(surveyData)
      });

      if (!response.ok) {
        throw new Error('Error al enviar la encuesta');
      }

      const data = await response.json();
      console.log('Encuesta creada:', data);
      alert('Encuesta enviada con éxito');

      navigate('/welcome'); // Redirigir tras el éxito
    } catch (error) {
      console.error(error);
      alert('Hubo un error al enviar la encuesta');
    }
  };

  return (
    <>
      <section className="box__father">
        <div className="box-white">
          {/* Nombre de la encuesta */}
          <input
            type="text"
            className="edit__survey__name"
            placeholder="New Survey"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />

          <div className="main">
            <div className="items__and__buttons">
              <button onClick={handleAddItem} className="create__new" title="Crear un nuevo item">
                New item +
              </button>

              <ol className="created__surveys">
                {items.map((item) => (
                  <SurveyItem
                    key={item.id}
                    item={item}
                    onInputChange={handleInputChange}
                    onRemoveItem={handleRemoveItem}
                  />
                ))}
              </ol>

              <div className="set__time">
                <p className="p__time">set time</p>
                <input
                  type="number"
                  min="0"
                  value={timeLimit}
                  onChange={(e) => setTimeLimit(Number(e.target.value))}
                  placeholder="0 = Sin límite"
                />
              </div>
            </div>

            <div className="separador"></div>

            <div className="visibilidad">
              <h2 className="title__vi">Visibilidad</h2>
              <p className="visi__p">¿Quién puede ver esta encuesta?</p>
              <ol className="visibilidad__list">
                <li className="visibilidad__item">
                  Publico
                  <input
                    type="checkbox"
                    checked={visibility.includes("Publico")}
                    onChange={() => handleVisibilityChange("Publico")}
                  />
                </li>
                
                <li className="visibilidad__item">
                  Privado
                  <input
                    type="checkbox"
                    checked={visibility.includes("Privado")}
                    onChange={() => handleVisibilityChange("Privado")}
                  />
                </li>
              </ol>
            </div>
          </div>
          <button className='todo__listo' onClick={handleSubmit}>Todo Listo</button>
        </div>
      </section>
    </>
  );
}

export default CreateSurvey;
