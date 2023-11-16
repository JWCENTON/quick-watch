import React from 'react';
import { Button, Spinner } from 'react-bootstrap';

export default function ClientDetail({ detailsData }) {
    return (
        <div className="details-section">
            {detailsData === null ? (
                <div className="spinner-container">
                    <Spinner animation="border" />
                </div>
            ) : (
                <div className="details-grid">
                    <div className="section-left">
                        <h4 className="details-header">Client Details</h4>
                        <p>First Name: {detailsData.firstName}</p>
                        <p>Last Name: {detailsData.lastName}</p>
                        <p>Phone Number: {detailsData.phoneNumber}</p>
                        <p>Email: {detailsData.email}</p>
                        <p>Contact Address: {detailsData.contactAddress}</p>
                        <div className="button-section">
                            <Button className="detail-view-btn">Edit</Button>
                            <Button className="detail-view-btn">Remove</Button>
                        </div>
                    </div>
                    <div className="section-right">
                        <h4 className="details-header">Assigned Commissions</h4>
                        <div className="button-section">
                            <Button className="detail-view-btn">Add Commission</Button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}