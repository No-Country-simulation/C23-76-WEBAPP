import React, { useState } from "react";
import axios from "axios";
import { motion } from "framer-motion";
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faKey, faEnvelope } from '@fortawesome/free-solid-svg-icons';
import './styles.css';

const Register = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({ email: "", password: "" });
    const [errorMessage, setErrorMessage] = useState("");
    const [successMessage, setSuccessMessage] = useState("");

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (formData.email !== "" && formData.password !== "") {
            try {
                // Realizamos la petición POST con Axios
                const response = await axios.post(
                    "https://surveymaker-53d73b4bd329.herokuapp.com/register",
                    formData,
                    {
                        headers: {
                            accept: "*/*",
                            "Content-Type": "application/json",
                        },
                    }
                );

                if (response.status === 200) {
                    setSuccessMessage("¡Registro exitoso!");
                    setErrorMessage("");
                    setTimeout(() => navigate("/login"), 1500);
                }
            } catch (error) {
                if (error.response?.status === 400) {
                    setErrorMessage("El email ya está en uso. Por favor, intenta con otro.");
                } else if (error.response?.status === 504) {
                    setErrorMessage("El servidor no responde. Por favor, intenta más tarde.");
                } else {
                    setErrorMessage("Ocurrió un error. Inténtalo nuevamente.");
                }
                setSuccessMessage("");
            }
        }
    };

    return (
        <>  
            <div className="form-container">
                <motion.div
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    exit={{ opacity: 0 }}
                    transition={{ duration: 0.5 }}>
                    <h2 className="form-title">Crear una cuenta</h2>
                    <form onSubmit={handleSubmit}>
                        <div className="input-container">
                            <div className="left">
                                <label htmlFor="email">Email</label>
                                <input
                                    type="email"
                                    id="email"
                                    name="email"
                                    placeholder="Ingrese su email"
                                    autoComplete="off"
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                            <FontAwesomeIcon icon={faEnvelope} size="lg" />
                        </div>
                        <div className="input-container">
                            <div className="left">
                                <label htmlFor="password">Contraseña</label>
                                <input
                                    type="password"
                                    id="password"
                                    name="password"
                                    placeholder="Ingrese su contraseña"
                                    autoComplete="off"
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                            <FontAwesomeIcon icon={faKey} size="lg" />
                        </div>
                        <button type="submit">Registrarse</button>
                        <p>
                            ¿Ya tienes una cuenta?{" "}
                            <b onClick={() => navigate("/login")}>Iniciar sesión</b>
                        </p>

                        {/* Mostrar mensajes de error o éxito */}
                    {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
                    {successMessage && <p style={{ color: "green" }}>{successMessage}</p>}
                    </form>
                </motion.div>
            </div>
        </>
    );
};

export default Register;
