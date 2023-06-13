import React from 'react';
import { Card } from 'react-bootstrap';
import './equipmentCard.css';

function EquipmentCard() {
    return (
        <>
            <Card style={{ width: "100%", justifyContent: "space-evenly", display: "flex", flexDirection: "row" }}>
                <div className='cardColumn'>
                    <Card.Title>ID</Card.Title>
                    <Card.Text>{12345}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title>Name</Card.Title>
                    <Card.Text>John Doe</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title>Status</Card.Title>
                    <Card.Text>In use</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title>Location</Card.Title>
                    <Card.Text>Warehouse</Card.Text>
                </div>
            </Card>
        </>
    );
}

export default EquipmentCard;