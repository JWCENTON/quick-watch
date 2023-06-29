import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import UniversalCard from '../card/Card';
import './CardList.css';

function CardList() {
    const [cards, setCards] = useState(null);
    const location = useLocation();
    let itemType;

    // eslint-disable-next-line default-case
    switch (location.pathname) {
        case '/equipment':
            itemType = 'equipment';
            break;
        case '/commissions':
            itemType = 'commissions';
            break;
        case '/clients':
            itemType = 'clients';
            break;
        case '/companies':
            itemType = 'companies';
            break;
    }

    useEffect(() => {
        // eslint-disable-next-line default-case
        switch (location.pathname) {
            case '/equipment':
                GetEquipmentData();
                break;
            case '/commissions':
                GetCommissionData();
                break;
            case '/clients':
                GetClientData();
                break;
            case '/companies':
                GetCompanyData();
                break;
        }
    }, [location]);

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {itemType}</a> | <a className="myAndAllSwitch" href="/" >All {itemType}</a>
            <div className="cardsContainer">
                {cards == null ? <p>Loading...</p> : cards.map((card, index) => (<UniversalCard key={index} data={card} dataType={itemType}></UniversalCard>))}
            </div>
        </div>
    );

    async function GetClientData() {
        const response = await fetch('https://localhost:7007/api/client');
        const data = await response.json();
        setCards(data);
    }

    async function GetCompanyData() {
        const response = await fetch('https://localhost:7007/api/company');
        const data = await response.json();
        setCards(data);
    }

    async function GetCommissionData() {
        const response = await fetch('https://localhost:7007/api/commission');
        const data = await response.json();
        setCards(data);
    }

    async function GetEquipmentData() {
        const response = await fetch('https://localhost:7007/api/equipment');
        const data = await response.json();
        setCards(data);
    }
}

export default CardList;
