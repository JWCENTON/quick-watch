import React from 'react';
import './Client.css';
import Searchbar from '../searchbar/Searchbar';

function ReactComponent() {
    return (
        <div>
            <a href="/" >My clients</a> | <a href="/" >All clients</a>
            <div>
                <Searchbar />
            </div>
        </div>
    );
}

export default ReactComponent;