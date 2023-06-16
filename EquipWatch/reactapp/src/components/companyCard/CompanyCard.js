import React from 'react';
import { Card } from 'react-bootstrap';
import './CompanyCard.css';

function EquipmentCard({ name, address, createdAt }) {
    return (
        <>
            <Card className="card">
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Company name</Card.Title>
                    <Card.Text>{name}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Address</Card.Title>
                    <Card.Text>{address}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Account created</Card.Title>
                    <Card.Text>{createdAt}</Card.Text>
                </div>
            </Card>
        </>
    );
}

export default EquipmentCard;