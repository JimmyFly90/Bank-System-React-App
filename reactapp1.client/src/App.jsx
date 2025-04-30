import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import About from './pages/About';
import BankAccount from './pages/BankAccount';
import WeatherForecast from './pages/WeatherForecast';
import './App.css';

function App() {
    
    function Navbar() {
        return (
            <nav>
                <Link to="/">Home</Link> |
                <Link to="/about">About</Link> |
                <Link to="/bankaccount">Bank Account</Link>
            </nav>
        );
    }

        return (
            <Router>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/about" element={<About />} /> 
                    <Route path="/bankaccount" element={<BankAccount />} />
                    <Route path="/weatherforecast" element={<WeatherForecast />} />
                </Routes>
            </Router>
        );
    
}


export default App;