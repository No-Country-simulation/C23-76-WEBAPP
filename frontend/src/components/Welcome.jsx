import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";

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
            <h1>¡Bienvenido/a!</h1>
            <p>Estás autenticado como: {userEmail}</p>
            <button onClick={handleLogout}>Cerrar sesión</button>
        </div>
    );
};

export default Welcome;
