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
export default function Home() {
    return <h1>Home Page</h1>;
}