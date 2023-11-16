import React from 'react';
import { Spinner } from 'react-bootstrap';

export default function EmployeeDetail({ detailsData }) {
    return (
        <div>
            {detailsData === null ? (
                <div className="spinner-container">
                    <Spinner animation="border" />
                </div>
            ) : (
                <div>
                    {/* Employee details content */}
                </div>
            )}
        </div>
    );
}
