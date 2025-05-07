import { NavLink } from "react-router-dom";

export default function MyAppNav()
{
    return (
        <nav>
            <NavLink to="/" end>
                Home
            </NavLink>
            <NavLink to="/bankaccount" end>
                Bank Account
            </NavLink>
            <NavLink to="/lineofcreditaccount" end>
                Line of Credit Account
            </NavLink>
        </nav>
    );
}