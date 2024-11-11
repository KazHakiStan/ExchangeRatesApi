import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { ExchangeRatesResponse } from '../types/ExchangeRate';
import './ExchangeRates.css';

const ExchangeRates: React.FC = () => {
    const [rates, setRates] = useState<ExchangeRatesResponse | null>(null);

    const fetchRates = async () => {
        try {
            const response = await axios.get('https://localhost:7264/ExchangeRate');
            setRates(response.data[0]);
        } catch (error) {
            console.error('Error fetching rates:', error);
        }
    };

    useEffect(() => {
        fetchRates();
    }, []);

    console.log(rates);
    console.log(rates?.rates);

    return (
        <div className="exchange-rates-container">
            <h2>Currency Exchange Rates</h2>
            {/*<button onClick={fetchRates}>Get Exchange Rates</button>*/}
            {rates?.rates && (
                <>
                    <p>
                        <strong>Table:</strong> {rates.table} <br/>
                        <strong>Number:</strong> {rates.no} <br />
                        <strong>Effective Date:</strong> {rates.effectiveDate}
                    </p>
                    <table className="exchange-rates-table">
                        <thead>
                            <tr>
                                <th>Currency</th>
                                <th>Code</th>
                                <th>Rate</th>
                            </tr>
                        </thead>
                        <tbody>
                            {rates.rates.map((rate) => (
                                <tr key={rate.code}>
                                    <td>{rate.currency}</td>
                                    <td>{rate.code}</td>
                                    <td>{rate.mid.toFixed(4)}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </>
            )}
        </div>
    );
};

export default ExchangeRates;