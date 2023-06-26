import React from 'react';
import './Clients.css';
import ClientCard from '../clientCard/ClientCard';

function Client() {
    return (
        <div>
            <a className="myAndAllSwitch" href="/" >My clients</a> | <a className="myAndAllSwitch" href="/" >All clients</a>
            <div className="clientContainer">
                <ClientCard ></ClientCard>
            </div>
        </div>
    );
}

export default Client;
