import React from 'react';
import { Button } from 'react-bootstrap';
import './Sidebar.css';

const Sidebar = () => {
    return (
        <div className="sidebar">
            <ul>
                <li>
                    <Button variant="outline-primary">Companies</Button>
                </li>
                <li>
                    <Button variant="outline-primary">Overview</Button>
                </li>
                <li>
                    <Button variant="outline-primary">Commissions</Button>
                </li>
                <li>
                    <Button variant="outline-primary">Equipment</Button>
                </li>
                <li>
                    <Button variant="outline-primary">Clients</Button>
                </li>
                <li>
                    <Button variant="outline-primary">Employees</Button>
                </li>
                <li>
                    <Button variant="outline-primary">Warehouses</Button>
                </li>
                <li>
                    <Button variant="outline-primary">Personal Info</Button>
                </li>
            </ul>
        </div>
    );
};

export default Sidebar;