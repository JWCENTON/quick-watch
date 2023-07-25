import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import UniversalCard from '../card/Card';
import './CardList.css';
import { useAuth } from '../authProvider/AuthContext';

function CardList() {
    const [cards, setCards] = useState(null);
    const location = useLocation();
    const { token } = useAuth(); 

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
        GetData(url, token);
    }, [location, token]);

    async function GetData(url, token) {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        const response = await fetch(url, { headers });
        const data = await response.json();
        setCards(data);
    }

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {displayedCategory}</a> | <a className="myAndAllSwitch" href="/" >All {displayedCategory}</a>
            <div className="cardsContainer">
                {cards == null ? <p>Loading...</p> : cards.map((card, index) => (<UniversalCard key={index} data={card} dataType={itemType}></UniversalCard>))}
            </div>
            <Button as={Link} to={`/${itemType}/create`}>Add New</Button>
        </div>
    );
}

export default CardList;
