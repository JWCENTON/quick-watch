import React from 'react';
import { Card } from 'react-bootstrap';
import './Card.css';

function CommissionCard({ id, clientName, address, type }) {
    return (
        <>
            <Card className="card">
                <div className='cardColumn'>
                    <Card.Title className='card-title'>ID</Card.Title>
                    <Card.Text>{id}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Client name</Card.Title>
                    <Card.Text>{clientName}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Address</Card.Title>
                    <Card.Text>{address}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Commission Type</Card.Title>
                    <Card.Text>{type}</Card.Text>
                </div>
            </Card>
        </>
    );
}

export default CommissionCard;