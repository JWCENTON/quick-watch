import React, { useState } from 'react';
import { Form, FormControl, Dropdown, InputGroup } from 'react-bootstrap';
import { BsSearch } from 'react-icons/bs';
import './Searchbar.css';

const Searchbar = () => {
    const [category, setCategory] = useState("Equipment");

    const handleSelect = (eventKey) => {
        setCategory(eventKey);
    }

    return (
        <Form inline className="ml-3">
            <InputGroup>
                <Dropdown onSelect={handleSelect}>
                    <Dropdown.Toggle variant="success" id="dropdown-basic">
                        {category}
                    </Dropdown.Toggle>

                    <Dropdown.Menu>
                        <Dropdown.Item eventKey="Equipment">Equipment</Dropdown.Item>
                        <Dropdown.Item eventKey="Commissions">Commissions</Dropdown.Item>
                        <Dropdown.Item eventKey="Clients">Clients</Dropdown.Item>
                    </Dropdown.Menu>
                </Dropdown>
                <FormControl type="text" placeholder="Search" className="mr-sm-2" />
                <InputGroup.Append>
                    <InputGroup.Text><BsSearch /></InputGroup.Text>
                </InputGroup.Append>
            </InputGroup>
        </Form>
    );
};

export default Searchbar;