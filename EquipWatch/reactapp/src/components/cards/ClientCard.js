import React from 'react';
import { Card } from 'react-bootstrap';
import './Card.css';
import { Link } from 'react-router-dom';

function ClientCard({ id, firstName, lastName, address, phone }) {
    return (
        <Link to={`/client/${id}`}>
            <Card className="card">
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Client Name</Card.Title>
                    <Card.Text>{firstName} {lastName}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Contact Address</Card.Title>
                    <Card.Text>{address}</Card.Text>
                </div>
                <div className='cardColumn'>
                    <Card.Title className='card-title'>Phone</Card.Title>
                    <Card.Text>{phone}</Card.Text>
                </div>
                {/*<div className='cardColumn'>*/}
                {/*    <Card.Title className='card-title'>Most Recent Commission</Card.Title>*/}
                {/*    <Card.Text>{recentCommission}</Card.Text>*/}
                {/*</div>*/}
            </Card>
        </Link>
    );
}

export default ClientCard;