import React, { useState, useEffect } from 'react';

export default function InterestEarningAccount() {
    const [balance, setBalance] = useState(0);
    const [logs, setLogs] = useState([]);
    const [newAccount, setNewAccount] = useState({ owner: '', initialBalance: 0 });
    const [accountNumber, setAccountNumber] = useState('');
    const [deposit, setDeposit] = useState({ amount: 0, note: '' });
    const [withdrawal, setWithdrawal] = useState({ amount: 0, note: '' });

    useEffect(() => {
        if (accountNumber) {
            populateInterestEarningAccount();
        }
    }, [accountNumber]);

    return (
        <div>
            <h1> Interest Earning Account Page</h1>
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

            <h2>Make a Withdrawal</h2>
            <input
                type="number"
                placeholder="Amount"
                value={withdrawal.amount}
                onChange={(e) => setWithdrawal({ ...withdrawal, amount: parseFloat(e.target.value) })}
            />
            <input
                type="text"
                placeholder="Note"
                value={withdrawal.note}
                onChange={(e) => setWithdrawal({ ...withdrawal, note: e.target.value })}
            />
            <button onClick={makeWithdrawal}>Withdraw</button>
        </div>
    );

    async function createAccount() {
        try {
            const response = await fetch(`/interestearningaccount/`, {
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

    async function populateInterestEarningAccount() {
        if (!accountNumber) return;
        const response = await fetch(`/interestearningaccount/${accountNumber}/logs`);
        if (response.ok) {
            const data = await response.json();
            setLogs(data);
        }
    }

    async function makeDeposit() {
        if (!accountNumber) {
            alert('Please create an account first.');
            return;
        }
        try {
            const response = await fetch(`/interestearningaccount/{accountNumber}/deposits`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(deposit),
            });
            if (response.ok) {
                const data = await response.json();
                setBalance(data.balance);
                setLogs(data.logs);
            } else {
                const errorData = await response.json();
                setLogs(errorData.Logs || []);
                console.error('Error making deposit:', response.statusText);
            }
        } catch (error) {
            console.error('Error making deposit:', error);
        }
    }

    async function makeWithdrawal() {
        if (!accountNumber) {
            alert('Please create an account first.');
            return;
        }
        try {
            const response = await fetch(`/interestearningaccount/{accountNumber}/withdrawals`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(withdrawal),
            });
            if (response.ok) {
                const data = await response.json();
                setBalance(data.balance);
                setLogs(data.logs);
            } else {
                const errorData = await response.json();
                setLogs(errorData.Logs || []);
                console.error('Error making withdrawal:', response.statusText);
            }
        } catch (error) {
            console.error('Error making withdrawal:', error);
        }
    }
}