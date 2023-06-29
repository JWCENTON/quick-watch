import React from 'react';
import { Card } from 'react-bootstrap';
import './Card.css';

function ClientCard({ data }) {
    return (
        <>
            <Card className="card">
                {
                    Object.keys(data).map((key, index) => (
                        <div className='cardColumn' key={ index }>
                            <Card.Title className='card-title'>{ key }</Card.Title>
                            <Card.Text>{ data[key] }</Card.Text>
                        </div>
                    ))
                }
            </Card>
        </>
    );
}

export default ClientCard;