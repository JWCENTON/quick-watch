import React from 'react';
import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './Card.css';

function UniversalCard({ data, dataType }) {
    return (
        <Link to={`/${dataType}/${data.id}`}>
            <Card className="card">
                {
                    Object.keys(data).map((key, index) => (
                        <div className='cardColumn' key={index}>
                            <Card.Title className='card-title'>{key}</Card.Title>
                            <Card.Text>{data[key]}</Card.Text>
                        </div>
                    ))
                }
            </Card>
        </Link>
    );
}

export default UniversalCard;