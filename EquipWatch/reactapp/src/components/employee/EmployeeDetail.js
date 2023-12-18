import React from 'react';
import './EmployeeDetail.css';
import { Spinner } from 'react-bootstrap';
import CommissionDetail from '../commission/CommissionDetail';

export default function EmployeeDetail({ detailsData }) {
    return (
        <div className="details-section">
            {detailsData === null ? (
                <div className="spinner-container">
                    <Spinner animation="border" />
                </div>
            ) : (
            <div class="main-content">
                <div class="details-section">
                    <div className="section-justified">
                        <h4 className="details-header">Employee Details</h4>
                        <p>First Name: {detailsData.firstName}</p>
                        <p>Last Name: {detailsData.lastName}</p>
                        <p>E-mail: {detailsData.email}</p>
                    </div>
                    <div className="section-justified">
                        <div className="section-justified">
                            <h4 className="details-header">Commission Details</h4>
                            <p>Location: {detailsData.location}</p>
                            <p>Description: {detailsData.description}</p>
                            <p>Scope: {detailsData.scope}</p>
                        </div>
                    </div>
                </div>
            </div>
            )}
        </div>
    );
}