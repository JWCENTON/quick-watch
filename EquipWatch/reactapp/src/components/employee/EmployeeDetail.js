//import React from 'react';
//import './EmployeeDetail.css';
//import { Spinner } from 'react-bootstrap';
//import CommissionDetail from '../commission/CommissionDetail';

//export default function EmployeeDetail({ detailsData }) {
//    return (
//        <div className="details-section">
//            {detailsData === null ? (
//                <div className="spinner-container">
//                    <Spinner animation="border" />
//                </div>
//            ) : (
//            <div class="main-content">
//                <div class="details-section">
//                    <div className="section-justified">
//                        <h4 className="details-header">Employee Details</h4>
//                        <p>First Name: {detailsData.firstName}</p>
//                        <p>Last Name: {detailsData.lastName}</p>
//                        <p>E-mail: {detailsData.email}</p>
//                    </div>
//                    <div className="section-justified">
//                        <div className="section-justified">
//                            <h4 className="details-header">Commission Details</h4>
//                            <p>Location: {detailsData.location}</p>
//                            <p>Description: {detailsData.description}</p>
//                            <p>Scope: {detailsData.scope}</p>
//                        </div>
//                    </div>
//                </div>
//            </div>
//            )}
//        </div>
//    );
//}

import React, { useState, useEffect } from 'react';
import { Spinner } from 'react-bootstrap';
import { useAuth } from '../../contexts/authProvider/AuthContext';

export default function EmployeeDetail({ detailsData }) {
    const [employeeState, setEmployeeState] = useState({
        equipments: null,
    });

    const { authAxios } = useAuth();

    useEffect(() => {
        async function fetchEmployeeDetails() {
            try {
                // Make an API call to fetch equipment assigned to the employee
                const equipmentsResponse = await authAxios.get(`/api/Employee/${detailsData.clientId}/equipments`);
                const equipmentsData = await equipmentsResponse.data;

                setEmployeeState({
                    equipments: equipmentsData,
                });
            } catch (error) {
                console.error('Error fetching employee details:', error);
            }
        }

        if (detailsData !== null) {
            fetchEmployeeDetails();
        }
    }, [detailsData]);

    return (
        <div className="details-section">
            {detailsData === null ? (
                <div className="spinner-container">
                    <Spinner animation="border" />
                </div>
            ) : (
                <div className="main-content">
                    <div className="details-section">
                        <div className="section-justified">
                            <h4 className="details-header">Employee Details</h4>
                            <p>First Name: {detailsData.firstName}</p>
                            <p>Last Name: {detailsData.lastName}</p>
                            <p>E-mail: {detailsData.email}</p>
                        </div>
                        {employeeState.equipments && (
                            <div className="section-justified">
                                <h4 className="details-header">Equipment Assigned</h4>
                                {employeeState.equipments.length === 0 ? (
                                    <p>No Equipment Assigned</p>
                                ) : (
                                    employeeState.equipments.map((equipment) => (
                                        <div key={equipment.id}>
                                            <p>Serial Number: {equipment.serialNumber}</p>
                                            <p>Category: {equipment.category}</p>
                                            <p>Location: {equipment.location}</p>
                                            {/* Add more details as needed */}
                                        </div>
                                    ))
                                )}
                            </div>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
}