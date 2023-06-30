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
        let url;
        // eslint-disable-next-line default-case
        switch (location.pathname) {
            case '/equipment':
                url = 'https://localhost:7007/api/equipment';
                break;
            case '/commissions':
                url = 'https://localhost:7007/api/commission';
                break;
            case '/clients':
                url = 'https://localhost:7007/api/client';
                break;
            case '/companies':
                url = 'https://localhost:7007/api/company';
                break;
            case 'employees':
                url = 'https://localhost:7007/api/employee';
                break;
        }
        GetData(url);
    }, [location]);

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {displayedCategory}</a> | <a className="myAndAllSwitch" href="/" >All {displayedCategory}</a>
            <div className="cardsContainer">
                {cards == null ? <p>Loading...</p> : cards.map((card, index) => (<UniversalCard key={index} data={card} dataType={itemType}></UniversalCard>))}
            </div>
        </div>
    );

    async function GetData(url) {
        const response = await fetch(url);
        const data = await response.json();
        setCards(data);
    }
}

export default CardList;
