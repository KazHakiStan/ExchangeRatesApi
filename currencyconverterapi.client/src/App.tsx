import React from 'react';
import './App.css';
import ExchangeRates from './components/ExchangeRates';

const App: React.FC = () => {
    return (
        <div className="App">
            <h1>Currency Exchange</h1>
            <ExchangeRates />
        </div>
    )
}

export default App;