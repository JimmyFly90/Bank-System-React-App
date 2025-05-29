import React, { useState, useEffect } from 'react';

export default function LineOfCreditAccount() {
    const [balance, setBalance] = useState(0);
    const [logs, setLogs] = useState([]);
    const [newAccount, setNewAccount] = useState({ owner: '', initialBalance: 0 });
    const [accountNumber, setAccountNumber] = useState('');
    const [payment, setPayment] = useState({ amount: 0, note: '' });

    useEffect(() => {
        if (accountNumber) {
            populateLineOfCreditAccount();
        }
    }, [accountNumber]);

    return (
        <div>
            <h1>Line of Credit Account Page</h1>
            <p>Balance: ${balance}</p>
            <h2>Logs</h2>
            <ul>
                {logs.map((log, index) => (
                    <li key={index}>
                        <strong>{log.timestamp}</strong>: {log.message}
                    </li>
                ))}
            </ul>

            <h2>Create New Account</h2>
            <input
                type="text"
                placeholder="Owner"
                value={newAccount.owner}
                onChange={(e) => setNewAccount({ ...newAccount, owner: e.target.value })}
            />
            <input
                type="number"
                placeholder="Inital Amount Borrowed"
                value={newAccount.initialBalance}
                onChange={(e) => setNewAccount({ ...newAccount, initialBalance: parseFloat(e.target.value) })}
            />
            <button onClick={createAccount}>Create Account</button>

            <h2>Make a Payment</h2>
            <input
                type="number"
                placeholder="Amount to pay"
                value={payment.amount}
                onChange={(e) => setPayment({ ...payment, amount: parseFloat(e.target.value) })}
            />
            <input
                type="text"
                placeholder="Note"
                value={payment.note}
                onChange={(e) => setPayment({ ...payment, note: e.target.value })}
            />
            <button onClick={makePayment}>Pay into Account</button>

        </div>
    );

    async function createAccount() {
        try {
            const response = await fetch(`/lineofcreditaccount`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newAccount),
            });
            if (response.ok) {
                const data = await response.json();
                setAccountNumber(data.accountNumber);
                setBalance(data.balance);
                setLogs(data.logs);
            } else {
                console.error('Error creating account:', response.statusText);
            }
        } catch (error) {
            console.error('Error creating account:', error);
        }
    }

    async function populateLineOfCreditAccount() {
        if (!accountNumber) return;
        const response = await fetch(`/lineofcreditaccount/${accountNumber}/logs`);
        if (response.ok) {
            const data = await response.json();
            setLogs(data);
        }
    }

    async function makePayment() {
        try {
            const response = await fetch(`/lineofcreditaccount/${accountNumber}/payments`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payment),
            });
            if (response.ok) {
                const data = await response.json();
                setBalance(data.balance);
                setLogs(data.logs);
            } else {
                const errorData = await response.json();
                setLogs(errorData.Logs || []);
                console.error('Error making payment:', response.statusText);
            }
        } catch (error) {
            console.error('Error making payment:', error);
        }
    }
}