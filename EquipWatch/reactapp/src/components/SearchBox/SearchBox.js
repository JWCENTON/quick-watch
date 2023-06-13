import React from 'react';
import { FaSearch } from 'react-icons/fa';
import "./Search.css";

function SearchBox() {
    return (
        <div className="search-box">
            <FaSearch className="search-icon" />
            <input
                type="text"
                placeholder="Search"
                className="search-input"
            />
        </div>
    );
}

export default SearchBox;