import React from 'react';
import SearchBox from '../SearchBox';

function ReactComponent() {
    return (
        <div className="background-color wrapper">
            <a href="/" >My clients</a>
            <a href="/" >All clients</a>
            <div>
                <SearchBox />
            </div>
        </div>
    );
}

export default ReactComponent;