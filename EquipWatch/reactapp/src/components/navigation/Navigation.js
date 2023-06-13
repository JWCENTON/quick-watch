import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import './Navigation.css';

const Navigation = () => {
    return (
        <Navbar bg="#112D4E" variant="dark">
            <Navbar.Brand >Home</Navbar.Brand>
            <Nav className="ml-auto">
                <Nav.Link >Logout</Nav.Link>
            </Nav>
        </Navbar>
    );
};

export default Navigation;