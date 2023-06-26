import React from 'react';
import { useLocation } from 'react-router-dom';
import EquipmentCard from '../equipmentCard/EquipmentCard'
import './CardList.css';

function CardList() {
    const location = useLocation();
    let itemType;

    switch (location.pathname) {
    case '/equipment':
        itemType = 'equipment';
        break;
    case '/commissions':
        itemType = 'commissions';
        break;
    case '/clients':
        itemType = 'clients';
    }

    return (
        <div>
            <a className="myAndAllSwitch" href="/" >My {itemType}</a> | <a className="myAndAllSwitch" href="/" >All {itemType}</a>
            <EquipmentCard id="123" name="Placeholder Name" status="Placeholder Status" location="Placeholder Location" />
        </div>
    );
}

export default CardList;