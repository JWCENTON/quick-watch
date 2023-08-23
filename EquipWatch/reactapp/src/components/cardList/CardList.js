import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import UniversalCard from '../card/Card';
import { useAuth } from '../authProvider/AuthContext';

function CardList() {
    const [cards, setCards] = useState(null);
    const location = useLocation();
    const { token } = useAuth();
    let displayedCategory;
    let itemType;
    let searchWords;

    if (location.search) {
        const searchParams = new URLSearchParams(location.search);
        const category = searchParams.get('category');
        const searchText = searchParams.get('searchText');
        searchWords = searchText.split(' ');
        displayedCategory = category ? category.toLowerCase() : '';
    } else {
        displayedCategory = location.pathname.slice(1);
    }

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
        default:
            break;
    }

    useEffect(() => {
        let url;
        // eslint-disable-next-line default-case
        switch (displayedCategory) {
            case 'equipment':
                url = 'https://localhost:7007/api/equipment';
                break;
            case 'commissions':
                url = 'https://localhost:7007/api/commission';
                break;
            case 'clients':
                url = 'https://localhost:7007/api/client';
                break;
            case 'companies':
                url = 'https://localhost:7007/api/company';
                break;
            case 'employees':
                url = 'https://localhost:7007/api/employee';
                break;
            default:
                break;
        }
        GetData(url, token);
    }, [location, token]);

    async function GetData(url, token) {
        const headers = {
            'Content-Type': 'application/json'
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        const response = await fetch(url, { headers });
        let data = await response.json();
        
        if (location.search) {
            switch (displayedCategory) {
                case 'equipment':
                    data = data.filter(item =>
                        searchWords.some(word =>
                            (item.category?.toLowerCase().includes(word.toLowerCase())) ||
                            (item.location?.toString().includes(word.toLowerCase())) ||
                            (item.serialNumber?.toString().includes(word.toLowerCase()))
                        )
                    );
                    break;
                case 'commissions':
                    data = data.filter(item =>
                        searchWords.some(word =>
                            (item.description?.toLowerCase().includes(word.toLowerCase())) ||
                            (item.location?.toLowerCase().includes(word.toLowerCase())) ||
                            (item.scope?.toLowerCase().includes(word.toLowerCase()))
                        )
                    );
                    break;
                case 'clients':
                    data = data.filter(item =>
                        searchWords.some(word =>
                            (item.firstName?.toLowerCase().includes(word.toLowerCase())) ||
                            (item.lastName?.toLowerCase().includes(word.toLowerCase())) ||
                            (item.phoneNumber?.includes(word)) ||
                            (item.email?.toLowerCase().includes(word.toLowerCase())) ||
                            (item.contactAddress?.toLowerCase().includes(word.toLowerCase()))
                        )
                    );
                    break;
                default:
                    break;
            }
        }

        const modifiedData = data.map(item => {
            if (displayedCategory === 'equipment') {
                return {
                    ...item,
                    available: item.available ? <span className="unicode-mark">&#x2705;</span> : <span className="unicode-mark">&#x274C;</span>,
                    inWarehouse: item.inWarehouse ? <span className="unicode-mark">&#x2705;</span> : <span className="unicode-mark">&#x274C;</span>,
                    condition: <span className="star">{`★`.repeat(item.condition)}<span className="dark-star">{`★`.repeat(5 - item.condition)}</span></span>
                };
            } else {
                return item;
            }
        });
        setCards(modifiedData);
    }

    return (
        <div className="cardSection">
            <a className="myAndAllSwitch" href="/" >My {displayedCategory}</a> | <a className="myAndAllSwitch" href="/" >All {displayedCategory}</a>
            <div className="cardsContainer">
                {cards == null ? <p>Loading...</p> : cards.map((card, index) => (
                    <UniversalCard key={index} data={card} dataType={itemType} insideCardList={true} />
                ))}
            </div>
            <div className="btn-section">
                <Button as={Link} to={`/${itemType}/create`} className="btn-addcard">Add New</Button>
            </div>
        </div>
    );
}

export default CardList;