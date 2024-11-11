import React, { useState } from 'react';
import axios from 'axios';
import { ExchangeRatesResponse, SingleRateResponse } from '../types/ExchangeRate';
import './ExchangeRates.css';

const ExchangeRates: React.FC = () => {
    const [rates, setRates] = useState<ExchangeRatesResponse | null>(null);
    const [singleRate, setSingleRate] = useState<SingleRateResponse | null>(null);
    const [table, setTable] = useState<string>('A');
    const [currency, setCurrency] = useState<string>('');
    const [date, setDate] = useState<string>('');

    const fetchRates = async () => {
        let endpoint = `https://localhost:7264/ExchangeRate/${table}`;
        if (currency) {
            endpoint += `?code=${currency}`;
        } else if (date) {
            endpoint = `https://localhost:7264/ExchangeRate/${table}/${date}`;
        }
        try {
            const response = await axios.get(endpoint);
            if (currency) {
                setSingleRate(response.data);
                setRates(null);
            } else {
                setRates(response.data[0]);
                setSingleRate(null);
            }
        } catch (error) {
            console.error('Error fetching rates:', error);
        }
    };

    const saveRates = async () => {
        try {
            await axios.post('https://localhost:7264/ExchangeRate/Save', rates.rates);
            alert('Rates saved successfully!');
        } catch (error) {
            console.error('Error saving rates', error);
            alert('Failed to save rates.');
        }
    };

    return (
        <div className="exchange-rates-container">
            <h1>NBP Currency Exchange Rates</h1>
            <div className="er-details">
                <div className="er-detail">
                    <label>Table: </label>
                    <select value={table} onChange={(e) => setTable(e.target.value)}>
                        <option value="A">Table A</option>
                        <option value="B">Table B</option>
                        <option value="C">Table C</option>
                    </select>
                </div>
                <div className="er-detail">
                    <label>Currency Code: </label>
                    <input
                        value={currency}
                        onChange={(e) => setCurrency(e.target.value)}
                        placeholder="Optional, e.g., USD"
                    />
                </div>
                <div className="er-detail">
                    <label>Date: </label>
                    <input
                        type="date"
                        value={date}
                        onChange={(e) => setDate(e.target.value)}
                        placeholder="Optional, e.g., 2023-10-01"
                    />
                </div>
                <button onClick={fetchRates}>Fetch Rates</button>
                <button onClick={saveRates}>Save Rates</button>
            </div>

            <h2>Exchange Rates:</h2>
            {rates && (
                <ul>
                    {rates && rates.rates.map((rate) => (
                        <li key={rate.code}>
                            {rate.currency} ({rate.code}): {rate.mid}
                        </li>
                    ))}
                </ul>
            )}

            {singleRate && (
                <div className="er-details">
                    <h3>
                        {singleRate.currency} ({singleRate.code})
                    </h3>
                    <ul>
                        {singleRate.rates.map((rate, index) => (
                            <li key={index}>
                                {rate.effectiveDate}: {rate.mid}
                            </li>
                        ))}
                    </ul>
                </div>
            )}
        </div>
    );
};

export default ExchangeRates;