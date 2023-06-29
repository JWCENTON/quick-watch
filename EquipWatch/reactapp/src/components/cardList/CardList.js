import React, { useState , useEffect} from 'react';
import { useLocation } from 'react-router-dom';
import EquipmentCard from '../cards/EquipmentCard';
import CompanyCard from '../cards/CompanyCard';
import ClientCard from '../cards/ClientCard';
import CommissionCard from '../cards/CommissionCard';
import Card from '../cards/Card';
import './CardList.css';

function CardList() {
    const [cards, setCards] = useState(null);
    const location = useLocation();
    let itemType;
    let CardComponent;

    // eslint-disable-next-line default-case
    switch (location.pathname) {
        case '/equipment':
            itemType = 'equipment';
            CardComponent = EquipmentCard;
            break;
        case '/commissions':
            itemType = 'commissions';
            CardComponent = CommissionCard;
            break;
        case '/clients':
            itemType = 'clients';
            CardComponent = ClientCard;
            break;
        case '/companies':
            itemType = 'companies';
            CardComponent = CompanyCard;
            break;
    }

    useEffect(() => {
        // eslint-disable-next-line default-case
        switch (location.pathname) {
            case '/equipment':
                break;
            case '/commissions':
                break;
            case '/clients':
                GetClientData();
                break;
            case '/companies':
                break;
        }
    }, []);

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {itemType}</a> | <a className="myAndAllSwitch" href="/" >All {itemType}</a>
            <div className="cardsContainer">
                {cards == null ? <p>Loading...</p> : cards.map((card, index) => (<Card key={index} data={card}></Card>))}         
            </div>
        </div>
    );

    async function GetClientData() {
        const response = await fetch('https://localhost:7007/api/client');
        const data = await response.json();
        setCards(data);
    }
}

export default CardList;