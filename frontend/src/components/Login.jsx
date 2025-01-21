import React from 'react'
import { useNavigate} from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faKey, faEnvelope } from '@fortawesome/free-solid-svg-icons';

import './styles.css';

const Login = () => {

    const navigate = useNavigate();

    return (
        <>
            <div className='form-container'>
                <h2 className='form-title'>Iniciar sesión</h2>
                <form>
                    <div className='input-container'>
                        <div className='left'>
                            <label htmlFor="email">Email</label>
                            <input 
                                type="email" 
                                id="email" 
                                name="email" 
                                placeholder='Ingrese su email' 
                                autoComplete='off' 
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
                            />
                        </div>
                        <FontAwesomeIcon icon={faKey} size="lg" />
                    </div>
                    <button type="submit">Ingresar</button>
                    <p>¿Aún no tienes una cuenta?
                        <b onClick={() => navigate("/")}>Registrarse</b>
                    </p>
                </form>
            </div>
        </>
    )
}

export default Login;