import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/home';
import About from './pages/about';
import BankAccount from './pages/bankaccount';
import WeatherForecast from './pages/weatherforecast';
import './App.css';

function App()
{
    return (
        <Router>
             <Routes>
                 <Route path="/" element={<Home />} />
                 <Route path="/about" element={<About />} /> 
                 <Route path="/bankaccount" element={<BankAccount />} />
                 <Route path="/weather" element={<WeatherForecast />} />
             </Routes>
         </Router>
     );
    
}

export default App;