import React from 'react';
import './PersonalInfo.css';
import { Link } from 'react-router-dom';

function PersonalInfo() {
    return (
        <div className="background-color wrapper">
            <h3>Personal Information</h3>
            <br/>
            <p><strong>First Name:</strong> Placeholder First Name</p>
            <p><strong>Last Name:</strong> Placeholder Last Name</p>
            <p><strong>Email:</strong> Placeholder Email</p>
            <br />
            <button type="button" className="info-button">Show Equipment</button>
            <button type="button" className="info-button">Show Commissions</button>
        </div>
    );
}

export default PersonalInfo;