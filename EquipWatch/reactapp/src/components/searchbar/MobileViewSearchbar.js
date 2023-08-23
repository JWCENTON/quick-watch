import React, { useState } from 'react';
import { Form, FormControl, Dropdown, InputGroup, Button } from 'react-bootstrap';
import { FaSearch } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';
import './MobileViewSearchbar.css';

const MobileViewSearchbar = ({ inSidebar }) => {
    const [category, setCategory] = useState("Equipment");
    const [searchText, setSearchText] = useState("");
    const navigate = useNavigate();

    const handleSelect = (eventKey) => {
        setCategory(eventKey);
    }

    const handleSearch = () => {
        const searchParams = new URLSearchParams({ category, searchText });
        navigate(`/search?${searchParams.toString()}`);
    }

    return (
        <Form inline="true" className={`search-bar ${inSidebar ? 'sidebar-search' : ''}`}>
            <div className="search-container">
                <InputGroup className="mb-2 search-input-group">
                    <FormControl
                        type="text"
                        placeholder="Search"
                        className="mr-sm-2"
                        value={searchText}
                        onChange={(e) => setSearchText(e.target.value)}
                    />
                    <Button variant="outline-primary" onClick={handleSearch}>
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