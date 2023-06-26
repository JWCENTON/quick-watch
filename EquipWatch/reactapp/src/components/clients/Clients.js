import React from 'react';
import './Clients.css';
import ClientCard from '../clientCard/ClientCard';

function Client() {

    let clients = GetClientData();

    return (
        <div className="clientSection">
            <a className="myAndAllSwitch" href="/" >My clients</a> | <a className="myAndAllSwitch" href="/" >All clients</a>
            <div className="clientContainer">
                {clients.map((client) => (<ClientCard name={client.Name} address={client.ContactAddress} phone={client.Phone} recentCommission={client.RecentCommission}></ClientCard>))}
            </div>
        </div>
    );
}

function GetClientData() {
    let clients = [{ "Name": "Oskar", "ContactAddress": "123 random road", "Phone": "Phone", "RecentCommission": "hello" }];
    return clients;
}

export default Client; 
