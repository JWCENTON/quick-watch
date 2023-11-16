import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { Button, Spinner } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import UniversalCard from '../card/Card';
import { useAuth } from '../authProvider/AuthContext';

function CardList() {
    const [cards, setCards] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const location = useLocation();
    const { authAxios } = useAuth();
    const apiUrl = process.env.REACT_APP_API_URL;
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
        switch (displayedCategory) {
            case 'equipment':
                url = `${apiUrl}/api/equipment`;
                break;
            case 'commissions':
                url = `${apiUrl}/api/commission`;
                break;
            case 'clients':
                url = `${apiUrl}/api/client`;
                break;
            case 'companies':
                url = `${apiUrl}/api/company`;
                break;
            case 'employees':
                url = `${apiUrl}/api/employee`;
                break;
            default:
                break;
        }
        GetData(url, authAxios);
    }, [location, authAxios]);

    async function GetData(url, authAxios) {
        setIsLoading(true);
        try {
            const response = await authAxios.get(url);
            let data = response.data;

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
                } else if (displayedCategory === 'commissions') {
                    return {
                        ...item,
                        endTime: item.endTime == null ? <span> Not specified </span> : <span> {item.endTime}</span>
                    };
                } else {
                    return item;
                }
            });
            setCards(modifiedData);
        } catch (error) {
            console.error('Error fetching data:', error);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="card-section">
            {isLoading ? (
                <div className="spinner-container">
                    <Spinner animation="border" />
                </div>
            ) : (
                <>
                    <div>
                        {cards && cards.map((card, index) => (
                            <UniversalCard key={index} data={card} dataType={itemType} insideCardList={true} />
                        ))}
                    </div>
                    <div className="btn-section">
                        <Button as={Link} to={`/${itemType}/create`} className="btn-addcard">Add New</Button>
                    </div>
                </>
            )}
        </div>
    );
}

export default CardList;