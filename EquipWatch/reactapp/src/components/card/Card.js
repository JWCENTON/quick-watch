import React, { useState, useEffect } from 'react';
import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './Card.css';

function UniversalCard({ data, dataType, insideCardList }) {
    const [windowWidth, setWindowWidth] = useState(window.innerWidth);
    const filteredData = Object.entries(data).filter(([key]) => key !== 'id');
    let numberOfItems = filteredData.length;

    useEffect(() => {
        const handleResize = () => {
            setWindowWidth(window.innerWidth);
        };

        window.addEventListener('resize', handleResize);

        return () => {
            window.removeEventListener('resize', handleResize);
        };
    }, []);

    if (windowWidth <= 768 && !insideCardList) {
        if (dataType !== "commission") {
            numberOfItems = 2;
        } else if (dataType === "commission") {
            numberOfItems = 1;
        }
    } else if (numberOfItems > 4) {
        numberOfItems = Math.ceil(numberOfItems / 2);
    }

    function camelCaseToSentence(str) {
        return str.replace(/([A-Z])/g, ' $1').split(' ').map(word => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase()).join(' ');
    }

    return (
        <Link to={`/${dataType}/${data.id}`}>
            <Card className="card" style={{ gridTemplateColumns: `repeat(${numberOfItems}, 1fr)` }}>
                {filteredData.map(([key, value]) => (
                    <div className='cardColumn' key={key}>
                        <Card.Title className='card-title'>{camelCaseToSentence(key)}</Card.Title>
                        <Card.Text className='card-text'>{value}</Card.Text>
                    </div>
                ))}
            </Card>
        </Link>
    );
}

export default UniversalCard;