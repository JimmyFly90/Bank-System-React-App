import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

function Navbar() {
    return (
        <nav>
            <Link to="/">Home</Link> |
            <Link to="/about">About</Link> |
            <Link to="/BankAccount">Bank Account</Link>
        </nav>
    );
}
export default function BankAccount() {
    const [balance, setBalance] = useState(0);
    const [logs, setLogs] = useState([]);

    useEffect(() => {
        fetch('/api/BankAccount')
            .then(response => response.json())
            .then(data => setBalance(data[0]?.balance || 0));

        fetch('/api/BankAccount/logs')
            .then(response => response.json())
            .then(data => setLogs(data));
    }, []);

    return (
        <div>
            <h1>Bank Account Page</h1>
            <p>Balance: ${balance}</p>
            <h2>Logs</h2>
            <ul>
                {logs.map((log, index) => (
                    <li key={index}>{log}</li>
                ))}
            </ul>
        </div>
    );
}