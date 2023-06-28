import React from 'react';
import CommissionCard from '../commissionCard/CommissionCard';

function ClientDetailView({ firstName, lastName, phoneNumber, email, contactAddress, commissions }) {
    return (
        <div>
            <h2>Client Details</h2>
            <p>First Name: {firstName}</p>
            <p>Last Name: {lastName}</p>
            <p>Phone Number: {phoneNumber}</p>
            <p>Email: {email}</p>
            <p>Contact Address: {contactAddress}</p>
            <h3>Assigned Commissions</h3>
            {commissions.map((commission, index) => (
                <CommissionCard key={index} {...commission} />
            ))}
            <button>Edit</button>
            <button>Remove</button>
        </div>
    );
}

export default ClientDetailView;