import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import avatar from "../assets/avatar.png";
import CardSurvey from "./CardSurvey";
import axios from "axios"; // Asegúrate de importar axios
import './welcome.css';

const Welcome = () => {
    const navigate = useNavigate();

    useEffect(() => {
        // Verificar si hay un email en el localStorage
        const userEmail = localStorage.getItem("userEmail");

        if (!userEmail) {
            // Si no hay un email, redirigir al login
            navigate("/login");
        }
    }, [navigate]);

    // Obtener el correo almacenado
    const userEmail = localStorage.getItem("userEmail");

    const handleLogout = () => {
      localStorage.removeItem("userEmail"); // Eliminar el token
      navigate("/login"); // Redirigir al login
  };

    return (
        <div className="welcome-container">            
            <aside>
                <header>
                    <div>
                        <img className="avatar" src={avatar} alt="Avatar" />
                        <h2>Bienvenido/a,</h2>
                        <h3 className="user-name">{userEmail}</h3>
                    </div>
                </header>
                <nav>
                    <div className="menu">
                        <button className="btn-menu">Crear nueva encuesta</button>
                        <button className="btn-menu">Encuestas activas</button>
                        <button className="btn-menu">Historial</button>
                    </div>
                    <div className="cnt-btn-logout">
                        <button className="btn-menu btn-logout" onClick={handleLogout}>Cerrar sesión</button>
                    </div>
                </nav>
            </aside>
            <main>
                <div className="cnt-survey">
                    <CardSurvey number="1" />
                    <CardSurvey number="2"/>
                    <CardSurvey number="3"/>
                    <CardSurvey number="4"/>
                    <CardSurvey number="5"/>
                    <CardSurvey number="6"/>
                    <CardSurvey number="7"/>
                </div>
            </main>
        </div>
    );
};

export default Welcome;
