import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/home';
import Navbar from './pages/Navbar';
import BankAccount from './pages/bankaccount';
import LineOfCreditAccount from './pages/lineofcreditaccount';
import InterestEarningAccount from './pages/interestearningaccount';
import WeatherForecast from './pages/weatherforecast';
import './App.css';

function App()
{
    return (
        <Router>
            <Navbar />
             <Routes>
                 <Route path="/" element={<Home />} />
                 <Route path="/bankaccount" element={<BankAccount />} />
                <Route path="/lineofcreditaccount" element={<LineOfCreditAccount />} />
                <Route path="/interestearningaccount" element={<InterestEarningAccount />} />
                 <Route path="/weather" element={<WeatherForecast />} />
             </Routes>
         </Router>
     );
}

export default App;