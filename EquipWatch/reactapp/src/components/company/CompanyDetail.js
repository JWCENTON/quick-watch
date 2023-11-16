import React from 'react';
import { Spinner } from 'react-bootstrap';

export default function CompanyDetail({ detailsData }) {
    return (
        <div>
            {detailsData === null ? (
                <div className="spinner-container">
                    <Spinner animation="border" />
                </div>
            ) : (
                <div>
                    {/* Company details content */}
                </div>
            )}
        </div>
    );
}
