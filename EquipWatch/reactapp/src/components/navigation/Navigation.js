import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import Searchbar from '../searchbar/Searchbar'
import './Navigation.css';

const Navigation = () => {
    return (
        <Navbar bg="#112D4E" variant="dark" expand="lg">
            <Navbar.Brand href="/">Home</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Nav className="ml-auto logout-link">
                <Nav.Link href="/">Logout</Nav.Link>
            </Nav>
            <Navbar.Collapse id="basic-navbar-nav">
                <Searchbar />
                
            </Navbar.Collapse>
            
        </Navbar>
    );
};

export default Navigation;