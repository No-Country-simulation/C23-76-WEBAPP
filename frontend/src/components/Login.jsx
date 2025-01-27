import React, { useState } from "react";
import axios from "axios";
import { motion } from "framer-motion";
import { useNavigate} from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faKey, faEnvelope } from '@fortawesome/free-solid-svg-icons';

import './styles.css';

const Login = () => {

    const [email, setEmail] = useState(""); 
    const [password, setPassword] = useState(""); 
    const [errorMessage, setErrorMessage] = useState(""); 
    const [successMessage, setSuccessMessage] = useState(""); 

    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault(); 

        const data = {
        email: email,
        password: password,
        };

        try {
        // Realizamos la petición POST con Axios
        const response = await axios.post(
            "https://surveymaker-53d73b4bd329.herokuapp.com/login", 
            data, // Datos del login
            {
            headers: {
                accept: "*/*", // Acepta cualquier tipo de respuesta
                "Content-Type": "application/json", // Enviamos un JSON
            },
            }
        );

        // Si la respuesta es exitosa (código 200)
        if (response.status === 200) {
            setSuccessMessage("¡Login exitoso!"); 
            setErrorMessage(""); 
            console.log(response.data); // Sirve para manejar el token u otros datos
            setTimeout(() => {
                navigate("/welcome");
            }, 1200);

        }
        } catch (error) {
        // Manejo de errores
        if (error.response && error.response.status === 401) {
            setErrorMessage("Por favor, verifica tu email y contraseña.");
        } else {
            setErrorMessage("Ocurrió un error. Inténtalo nuevamente.");
        }
        setSuccessMessage("");
        }
    };

    return (
        <>
            <div className='form-container'>
            <motion.div
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    exit={{ opacity: 0 }}
                    transition={{ duration: 0.5 }}>
                <h2 className='form-title'>Iniciar sesión</h2>
                <form onSubmit={handleSubmit}>
                    <div className='input-container'>
                        <div className='left'>
                            <label htmlFor="email">Email</label>
                            <input 
                                type="email" 
                                id="email" 
                                name="email" 
                                placeholder='Ingrese su email' 
                                autoComplete='off' 
                                onChange={(e) => setEmail(e.target.value)} // Actualizamos el estado del email
                                required
                            /> 
                        </div>
                        <FontAwesomeIcon icon={faEnvelope} size="lg" />
                    </div>
                    <div className='input-container'>
                        <div className='left'>
                            <label htmlFor="password">Contraseña</label>
                            <input 
                                type="password" 
                                id="password" 
                                name="password" 
                                placeholder='Ingrese su contraseña' 
                                autoComplete='off' 
                                onChange={(e) => setPassword(e.target.value)} // Actualizamos el estado de la contraseña
                                required
                            />
                        </div>
                        <FontAwesomeIcon icon={faKey} size="lg" />
                    </div>
                    <button type="submit">Ingresar</button>
                    <p>¿Aún no tienes una cuenta?
                        <b onClick={() => navigate("/")}>Registrarse</b>
                    </p>
                    {/* Mostrar mensajes de error o éxito */}
                    {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
                    {successMessage && <p style={{ color: "green" }}>{successMessage}</p>}
                </form>
                </motion.div>
            </div>
        </>
    )
}

export default Login;