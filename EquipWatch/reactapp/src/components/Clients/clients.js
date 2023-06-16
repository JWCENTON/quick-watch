import React from 'react';
import './Client.css';
import SearchBox from '../SearchBox/SearchBox';

function ReactComponent() {
    return (
        <div>
            <a href="/" >My clients</a> | <a href="/" >All clients</a>
            <div>
                <SearchBox />
            </div>
        </div>
    );
}

export default ReactComponent;