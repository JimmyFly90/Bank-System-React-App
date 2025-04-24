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
    console.log("Bank Account page");
    return (<h1>Bank Account Page</h1>);
}