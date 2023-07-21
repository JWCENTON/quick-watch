import React, { useState } from 'react';
import { Form, FormControl, Dropdown, InputGroup, Button } from 'react-bootstrap';
import { FaSearch } from 'react-icons/fa';
import './MobileViewSearchbar.css';

const MobileViewSearchbar = ({ inSidebar }) => {
    const [category, setCategory] = useState("Equipment");

    const handleSelect = (eventKey) => {
        setCategory(eventKey);
    }

    return (
        <Form inline="true" className={`search-bar ${inSidebar ? 'sidebar-search' : ''}`}>
            <div className="search-container">
                <InputGroup className="mb-2 search-input-group">
                    <FormControl type="text" placeholder="Search" className="search-field" />
                    <Button variant="outline-primary" className="search-button">
                        <FaSearch />
                    </Button>
                </InputGroup>
                <InputGroup className="mb-2">
                    <div className="dropdown-wrapper">
                        <Dropdown onSelect={handleSelect}>
                            <Dropdown.Toggle variant="secondary" align="end" id="dropdown-menu-align-end" className="category-dropdown">
                                {category}
                            </Dropdown.Toggle>
                            <Dropdown.Menu className="dropdown-menu">
                                <Dropdown.Item eventKey="Equipment">Equipment</Dropdown.Item>
                                <Dropdown.Item eventKey="Commissions">Commissions</Dropdown.Item>
                                <Dropdown.Item eventKey="Clients">Clients</Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                    </div>
                </InputGroup>
            </div>
        </Form>
    );
};

export default MobileViewSearchbar;