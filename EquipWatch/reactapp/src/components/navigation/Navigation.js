import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import Searchbar from '../searchbar/Searchbar'
import './Navigation.css';

const Navigation = () => {
    return (
        <Navbar bg="#112D4E" variant="dark">
            <Navbar.Brand href="/">Home</Navbar.Brand>
            <Searchbar />
            <Nav className="ml-auto">
                <Nav.Link href="/">Logout</Nav.Link>
            </Nav>
        </Navbar>
    );
};

export default Navigation;