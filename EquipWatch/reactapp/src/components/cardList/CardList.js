import React, { useState , useEffect} from 'react';
import { useLocation } from 'react-router-dom';
import Card from '../cards/Card';
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
        }
        GetData(url);
    }, [location]);

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {itemType}</a> | <a className="myAndAllSwitch" href="/" >All {itemType}</a>
            <div className="cardsContainer">
                {cards == null ? <p>Loading...</p> : cards.map((card, index) => (<Card key={index} data={card}></Card>))}         
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