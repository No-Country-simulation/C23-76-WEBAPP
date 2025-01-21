import React from 'react'
import { useNavigate} from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faKey, faEnvelope } from '@fortawesome/free-solid-svg-icons';

import './styles.css';

const Register = () => {

    const navigate = useNavigate();

    return (
        <>
            <div className='form-container'>
                <h2 className='form-title'>Crear una cuenta</h2>
                <form>
                    <div className='input-container'>
                        <div className='left'>
                            <label htmlFor="email">Email</label>
                            <input 
                                type="email" 
                                id="email" 
                                ame="email" 
                                placeholder='Ingrese su email' 
                                autoComplete='off' 
                            /> 
                        </div>
                        <FontAwesomeIcon icon={faEnvelope} size="lg" />
                    </div>
                    <div className='input-container'>
                        <div className='left'>
                            <label htmlFor="contraseña">Contraseña</label>
                            <input 
                                type="password" 
                                id="contraseña" 
                                ame="contraseñña" 
                                placeholder='Ingrese su contraseña' 
                                autoComplete='off' 
                            />
                        </div>
                        <FontAwesomeIcon icon={faKey} size="lg" />
                    </div>
                    <button type="submit">Registrarse</button>
                    <p>¿Ya tienes una cuenta?
                        <b onClick={() => navigate("/login")}>Iniciar sesión</b>
                    </p>
                </form>
            </div>
        </>
    )
}

export default Register;
