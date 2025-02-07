import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import avatar from "../assets/avatar.png";
import CardSurvey from "./CardSurvey";
import SurveyClick from "./componentes-juanze/survey_click/survey_click"; // Importa el nuevo componente
import axios from "axios";
import "./welcome.css";

const Welcome = () => {
    const navigate = useNavigate();
    const [surveys, setSurveys] = useState([]);
    const [selectedSurvey, setSelectedSurvey] = useState(null); // Estado para la encuesta seleccionada

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (!token) {
            navigate("/login");
            return;
        }

        console.log("Token almacenado:", token); // 📌 Verificar si el token existe

        axios.post("https://surveymaker-53d73b4bd329.herokuapp.com/survey/list/private", {
            withQuestions: true,
            withOptions: true
        }, {
            headers: { Authorization: `Bearer ${token}` }
        })
        .then((response) => {
            console.log("Encuestas recibidas:", response.data);
            setSurveys(response.data);
        })
        .catch((error) => {
            console.error("Error al obtener encuestas:", error.response ? error.response.data : error.message);
        });
    }, [navigate]);

    const handleLogout = () => {
        localStorage.removeItem("token");
        navigate("/login");
    };

    return (
        <div className="welcome-container">
            <aside>
                <header>
                    <div>
                        <img className="avatar" src={avatar} alt="Avatar" />
                        <h2>Bienvenido/a,</h2>
                        <h3 className="user-name">{localStorage.getItem("userEmail")}</h3>
                    </div>
                </header>
                <nav>
                    <div className="menu">
                        <button onClick={() => navigate('/create-survey')} className="btn-menu">Crear nueva encuesta</button>
                        <button className="btn-menu">Encuestas activas</button>
                    </div>
                    <div className="cnt-btn-logout">
                        <button className="btn-menu btn-logout" onClick={handleLogout}>Cerrar sesión</button>
                    </div>
                </nav>
            </aside>
            <main>
                <div className="cnt-survey">
                    {selectedSurvey ? (
                        <SurveyClick 
                            title={selectedSurvey.title} 
                            questions={selectedSurvey.questions || []}
                            tiempo={selectedSurvey.expiresAt ? `Expira en ${new Date(selectedSurvey.expiresAt).toLocaleString()}` : "Sin límite"}
                            onClose={() => setSelectedSurvey(null)} // Permite volver atrás
                        />
                    ) : (
                        surveys.length > 0 ? (
                            surveys.map((survey) => (
                                <CardSurvey 
                                    key={survey.id} 
                                    title={survey.title} 
                                    questions={survey.questions || []}
                                    tiempo={survey.expiresAt ? `Expira en ${new Date(survey.expiresAt).toLocaleString()}` : "Sin límite"}
                                    onClick={() => setSelectedSurvey(survey)} // Maneja el clic en la card
                                />
                            ))
                        ) : (
                            <p>No hay encuestas disponibles</p>
                        )
                    )}
                </div>
            </main>
        </div>
    );
};

export default Welcome;
