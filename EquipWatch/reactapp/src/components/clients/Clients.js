import React from 'react';
import './Clients.css';

function Client() {
    return (
        <div>
            <a className="myAndAllSwitch" href="/" >My clients</a> | <a className="myAndAllSwitch" href="/" >All clients</a>
            <div className="clientContainer">
            </div>
        </div>
    );
}

export default Client;
