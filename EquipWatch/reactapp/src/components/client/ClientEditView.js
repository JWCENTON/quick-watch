import React, { useState } from 'react';
import CommissionCard from '../cards/CommissionCard';

function ClientEditView({ firstName, lastName, phoneNumber, email, contactAddress, commissions }) {
    const [newFirstName, setNewFirstName] = useState(firstName);
    const [newLastName, setNewLastName] = useState(lastName);
    const [newPhoneNumber, setNewPhoneNumber] = useState(phoneNumber);
    const [newEmail, setNewEmail] = useState(email);
    const [newContactAddress, setNewContactAddress] = useState(contactAddress);

    return (
        <div>
            <h2>Edit Client</h2>
            <label>
                First Name:
                <input type="text" value={newFirstName} onChange={e => setNewFirstName(e.target.value)} />
            </label>
            <label>
                Last Name:
                <input type="text" value={newLastName} onChange={e => setNewLastName(e.target.value)} />
            </label>
            <label>
                Phone Number:
                <input type="text" value={newPhoneNumber} onChange={e => setNewPhoneNumber(e.target.value)} />
            </label>
            <label>
                Email:
                <input type="text" value={newEmail} onChange={e => setNewEmail(e.target.value)} />
            </label>
            <label>
                Contact Address:
                <input type="text" value={newContactAddress} onChange={e => setNewContactAddress(e.target.value)} />
            </label>
            <h3>Assigned Commissions</h3>
            {commissions.map((commission, index) => (
                <CommissionCard key={index} {...commission} />
            ))}
            <button>Add Commission</button>
            <button>Save</button>
        </div>
    );
}

export default ClientEditView;