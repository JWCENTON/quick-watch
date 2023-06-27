import React from 'react';
import { Button } from 'react-bootstrap';
import { Card } from 'react-bootstrap';
import './Card.css';
import { Link } from 'react-router-dom';

function ClientCard({ name, address, phone, recentCommission }) {
    return (
        <>
            <Card className="card">
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Client Name</Card.Title>
                    <Card.Text>{name}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Contact Address</Card.Title>
                    <Card.Text>{address}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Phone</Card.Title>
                    <Card.Text>{phone}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Most Recent Commission</Card.Title>
                    <Card.Text>{recentCommission}</Card.Text>
                </div>
            </Card>
        </>
    );
}

export default ClientCard;