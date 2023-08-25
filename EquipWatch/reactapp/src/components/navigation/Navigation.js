import React, { useContext } from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import Searchbar from '../searchbar/Searchbar';
import { SidebarContext } from '../../contexts/SidebarContext';
import { BiMenuAltRight } from 'react-icons/bi';
import './Navigation.css';

const Navigation = () => {
    const { toggleSidebar } = useContext(SidebarContext);

    return (
        <Navbar bg="#112D4E" variant="dark" expand="lg" className="justify-content-between">
            <div className="left-nav">
                <Navbar.Brand className="sidebar-toggle-button" onClick={toggleSidebar}>
                    <BiMenuAltRight size={30} />
                </Navbar.Brand>
                <Navbar.Brand href="/commissions">Home</Navbar.Brand>
            </div>
            <Searchbar />
            <Nav className="ml-auto logout-link">
                <Nav.Link href="/">Logout</Nav.Link>
            </Nav>
        </Navbar>
    );
};

export default Navigation;