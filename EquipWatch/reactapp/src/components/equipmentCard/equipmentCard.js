import React from 'react';
import { Card } from 'react-bootstrap';
import './EquipmentCard.css';

function EquipmentCard({ id, name, status, location }) {
    return (
        <>
            <Card style={{ width: "100%", justifyContent: "space-evenly", display: "flex", flexDirection: "row" }}>
                <div className='cardColumn'>
                    <Card.Title>ID</Card.Title>
                    <Card.Text>{id}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title>Name</Card.Title>
                    <Card.Text>{name}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title>Status</Card.Title>
                    <Card.Text>{status}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title>Location</Card.Title>
                    <Card.Text>{location}</Card.Text>
                </div>
            </Card>
        </>
    );
}

export default EquipmentCard;