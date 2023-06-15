import React, { useState } from 'react';
import { Form, FormControl, Dropdown, InputGroup, Button } from 'react-bootstrap';
import './Searchbar.css';

const Searchbar = () => {
    const [category, setCategory] = useState("Equipment");

    const handleSelect = (eventKey) => {
        setCategory(eventKey);
    }

    return (
        <Form inline="true" className="ml-3 search-bar">

            <InputGroup>
                <FormControl type="text" placeholder="Search" className="mr-sm-2" />
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
                <Button variant="outline-primary">Search</Button>
            </InputGroup>
        </Form>
    );
};

export default Searchbar;