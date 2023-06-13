import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';

const Sidebar = () => {
    return (
        <div className="sidebar" style={{ backgroundColor: "#F9F7F7" }}>
            <ul>
                <li>
                    <Button variant="outline-primary" as={Link} to="/">Companies</Button>
                </li>
                <li>
                    <Button variant="outline-primary" as={Link} to="/">Overview</Button>
                </li>
                <li>
                    <Button variant="outline-primary" as={Link} to="/">Commissions</Button>
                </li>
                <li>
                    <Button variant="outline-primary" as={Link} to="/">Equipment</Button>
                </li>
                <li>
                    <Button variant="outline-primary" as={Link} to="/">Clients</Button>
                </li>
                <li>
                    <Button variant="outline-primary" as={Link} to="/">Employees</Button>
                </li>
                <li>
                    <Button variant="outline-primary" as={Link} to="/">Warehouses</Button>
                </li>
                <li>
                    <Button variant="outline-primary" as={Link} to="/">Personal Info</Button>
                </li>
            </ul>
        </div>
    );
};

export default Sidebar;