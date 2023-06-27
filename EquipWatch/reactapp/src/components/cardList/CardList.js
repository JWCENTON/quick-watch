import React, { useState , useEffect} from 'react';
import { useLocation } from 'react-router-dom';
import EquipmentCard from '../cards/EquipmentCard';
import CompanyCard from '../cards/CompanyCard';
import ClientCard from '../cards/ClientCard';
import CommissionCard from '../cards/CommissionCard';
import './CardList.css';

function CardList() {
    const [cards, setCards] = useState(null);
    const location = useLocation();
    let itemType;
    let CardComponent;

    useEffect(() => {
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
                GetClientData();
                break;
            case '/companies':
                itemType = 'companies';
                CardComponent = CompanyCard;
                break;
        }
    }, []);

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {itemType}</a> | <a className="myAndAllSwitch" href="/" >All {itemType}</a>
            <div className="cardsContainer">
                {cards == null ? <p>Loading...</p> : cards.map((client, index) => (<ClientCard key={index} name={client.Name} address={client.ContactAddress} phone={client.Phone} recentCommission={client.RecentCommission}></ClientCard>))}         
            </div>
        </div>
    );

    async function GetClientData() {
        const response = await fetch('https://localhost:7007/api/client');
        console.log(response);
        const data = await response.json();
        console.log(data);
        setCards(data);
    }
}

export default CardList;