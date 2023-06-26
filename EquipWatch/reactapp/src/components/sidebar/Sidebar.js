import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import './Sidebar.css';

const Sidebar = () => {
    return (
        <div className="sidebar">
            <ul>
                <li>
                    <Button as={Link} to="/companies" variant="outline-primary">Companies</Button>
                </li>
                <li>
                    <Button as={Link} to="/company" variant="outline-primary">Company Overview</Button>
                </li>
                <li>
                    <Button as={Link} to="/" variant="outline-primary">Employee Overview</Button>
                </li>
                <li>
                    <Button as={Link} to="/commissions" variant="outline-primary">Commissions</Button>
                </li>
                <li>
                    <Button as={Link} to="/equipment" variant="outline-primary">Equipment</Button>
                </li>
                <li>
                    <Button as={Link} to="/clients" variant="outline-primary">Clients</Button>
                </li>
                {/*<li>*/}
                {/*    <Button variant="outline-primary">Employees</Button>*/}
                {/*</li>*/}
                {/*<li>*/}
                {/*    <Button variant="outline-primary">Warehouses</Button>*/}
                {/*</li>*/}
                <li>
                    <Button as={Link} to="/personalInfo" variant="outline-primary">Personal Info</Button>
                </li>
            </ul>
        </div>
    );
};

export default Sidebar;