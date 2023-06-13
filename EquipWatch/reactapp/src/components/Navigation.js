import React from 'react';
import { Link } from 'react-router-dom';
import { Navbar, Nav } from 'react-bootstrap';

const Navigation = () => {
    return (
        <Navbar bg="#112D4E" variant="dark">
            <Navbar.Brand as={Link} to="/">Home</Navbar.Brand>
            <Nav className="ml-auto">
                <Nav.Link as={Link} to="/">Logout</Nav.Link>
            </Nav>
        </Navbar>
    );
};

export default Navigation;