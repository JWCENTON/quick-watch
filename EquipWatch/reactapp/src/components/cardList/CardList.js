import React from 'react';
import { useLocation } from 'react-router-dom';
import EquipmentCard from '../cards/EquipmentCard';
import CompanyCard from '../cards/CompanyCard';
import ClientCard from '../cards/ClientCard';
import CommissionCard from '../cards/CommissionCard';
import './CardList.css';

function CardList() {
    const location = useLocation();
    let itemType;
    let CardComponent;

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

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {itemType}</a> | <a className="myAndAllSwitch" href="/" >All {itemType}</a>
            <div className="cardsContainer">
                <CardComponent id="123" name="Placeholder Name" address="Placeholder Address" clientName="Placeholder Client Name" status="Placeholder Status" location="Placeholder Location" phone="Placeholder Phone" createdAt="Placeholder Date" recentCommission="Placeholder Commission" />
                <CardComponent id="123" name="Placeholder Name" address="Placeholder Address" clientName="Placeholder Client Name" status="Placeholder Status" location="Placeholder Location" phone="Placeholder Phone" createdAt="Placeholder Date" recentCommission="Placeholder Commission" />
            </div>
        </div>
    );
}

export default CardList;