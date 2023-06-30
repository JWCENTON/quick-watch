import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import UniversalCard from '../card/Card';
import './CardList.css';

function CardList() {
    const [cards, setCards] = useState(null);
    const location = useLocation();
    let itemType;

    let displayedCategory = location.pathname.slice(1);

    // eslint-disable-next-line default-case
    switch (displayedCategory) {
        case 'equipment':
            itemType = 'equipment';
            break;
        case 'commissions':
            itemType = 'commission';
            break;
        case 'clients':
            itemType = 'client';
            break;
        case 'companies':
            itemType = 'company';
            break;
        case 'employees':
            itemType = 'employee';
            break;
    }

    useEffect(() => {
        // eslint-disable-next-line default-case
        switch (displayedCategory) {
            case 'equipment':
                GetEquipmentData();
                break;
            case 'commissions':
                GetCommissionData();
                break;
            case 'clients':
                GetClientData();
                break;
            case 'companies':
                GetCompanyData();
                break;
            case 'employees':
                GetEmployeesData();
                break;
        }
    }, [location]);

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {displayedCategory}</a> | <a className="myAndAllSwitch" href="/" >All {displayedCategory}</a>
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

    async function GetEmployeesData() {
        const response = await fetch('https://localhost:7007/api/employee');
        const data = await response.json();
        setCards(data);
    }
}

export default CardList;
