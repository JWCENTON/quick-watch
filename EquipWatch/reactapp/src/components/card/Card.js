import React from 'react';
import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './Card.css';

function UniversalCard({ data, dataType }) {
	 const filteredData = Object.entries(data).filter(([key]) => key !== 'id');
    return (
        <Link to={`/${dataType}/${data.id}`}>
            <Card className="card">
                {filteredData.map(([key, value]) => (
				<div className='cardColumn' key={key}>
				<Card.Title className='card-title'>{key}</Card.Title>
				<Card.Text>{value}</Card.Text>
          </div>
        ))}
            </Card>
        </Link>
    );
}

export default UniversalCard;