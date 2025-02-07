import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Register from "./components/Register";
import Welcome from "./components/Welcome";
import PrivateRoute from "./PrivateRoute";
import './App.css';

// juanze
import CreateSurvey from './components/componentes-juanze/menu de creacion/creation_&_edition_menu'
import Survey from "./components/componentes-juanze/menu de creacion/encuesta-activa/encuesta";
import Survey_click from "./components/componentes-juanze/survey_click/survey_click";


const App = () =>{
    return <BrowserRouter>
        <div className="container">
            <Routes>
                <Route path="/" element={<Register />} />
                <Route path="/login" element={<Login />} />
               
            
                <Route element={<PrivateRoute />}>
                    <Route path="/welcome" element={<Welcome />} />
                    {/* juanze */}
                    <Route path="/create-survey" element={<CreateSurvey />}/>
                    <Route path="/survey" element={<Survey/>}/>
                    <Route path="/encuesta" element={<Survey_click/>}/>
                   
                </Route>
            </Routes>
        </div>
    </BrowserRouter>;
}

export default App;
