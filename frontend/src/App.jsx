import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Register from "./components/Register";
import Welcome from "./components/Welcome";
import './App.css';

const App = () =>{
    return <BrowserRouter>
        <div className="container">
            <Routes>
                <Route path="/" element={<Register/>} />
                <Route path="/login" element={<Login />} />

                <Route path="/welcome" element={<Welcome />} />
            </Routes>
        </div>
    </BrowserRouter>;
}

export default App;
