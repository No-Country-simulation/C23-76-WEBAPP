import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Register from "./components/Register";
import './App.css';

const App = () =>{
    return <BrowserRouter>
        <div className="container">
            <Routes>
                <Route path="/" element={<Register/>} />
                <Route path="/login" element={<Login />} />
            </Routes>
        </div>
    </BrowserRouter>;
}

export default App;
