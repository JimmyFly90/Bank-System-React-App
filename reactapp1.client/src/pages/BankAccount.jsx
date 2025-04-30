import React, { useState, useEffect } from 'react';

export default function BankAccount() {
    const [balance, setBalance] = useState(0);
    const [logs, setLogs] = useState([]);
    const [newAccount, setNewAccount] = useState({ owner: '', initialBalance: 0 });
    const [deposit, setDeposit] = useState({ amount: 0, note: '' });

    useEffect(() => {
        (async () => {
            populateBankAccount();
        })();
    }, []);

    return (
        <div>
            <h1>Bank Account Page</h1>
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
                placeholder="Inital Balance"
                value={newAccount.initialBalance}
                onChange={(e) => setNewAccount({ ...newAccount, initialBalance: parseFloat(e.target.value) })}
            />
            <button onClick={createAccount}>Create Account</button>

            <h2>Make a Deposit</h2>
            <input
                type="number"
                placeholder="Amount"
                value={deposit.amount}
                onChange={(e) => setDeposit({ ...deposit, amount: parseFloat(e.target.value) })}
            />
            <input
                type="text"
                placeholder="Note"
                value={deposit.note}
                onChange={(e) => setDeposit({ ...deposit, note: e.target.value })}
            />
            <button onClick={makeDeposit}>Deposit</button>
        </div>
    );

    async function populateBankAccount() {
        const response = await fetch('/api/bankaccount');
        if (response.ok) {
            const data = await response.json();
            setBalance(data[0]?.balance || 0);
            setLogs(data);
        }
    }

    async function createAccount() {
        try {
            const response = await fetch('/api/bankaccount', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newAccount),
            });
            if (response.ok) {
                const data = await response.json();
                console.log('Account created:', data);
                setBalance(data.balance);
                setLogs((prev) => [...prev, `Account created for ${data.owner} with balance $${data.balance}`]);
            } else {
                console.error('Error creating account:', response.statusText);
            }
        } catch (error) {
            console.error('Error creating account:', error);
        }
    }
    async function makeDeposit() {
        try {
            const response = await fetch('/api/bankaccount/deposit', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(deposit),
            });
            if (response.ok) {
                const data = await response.json();
                console.log('Deposit successful:', data);
                setBalance(data.balance);
                setLogs((prev) => [...prev, `Deposit of $${data.amount} made. Note: ${data.note}`]);
            } else {
                console.error('Error making deposit:', response.statusText);
            }
        } catch (error) {
            console.error('Error making deposit:', error);
        }
    }
}