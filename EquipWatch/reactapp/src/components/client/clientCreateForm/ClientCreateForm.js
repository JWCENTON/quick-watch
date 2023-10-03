import React from 'react';
import { useState } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import './clientCreateForm.css';
import { useAuth } from '../../authProvider/AuthContext';

export default function ClientCreateForm() {
    const apiUrl = process.env.REACT_APP_API_URL;
    const navigate = useNavigate();
    const { token } = useAuth();
	const [errorMessage, setErrorMessage] = useState('');

    async function handleSubmit(event) {
        event.preventDefault();
        let formFirstName = event.target.firstName.value;
        let formLastName = event.target.lastName.value;
        let formEmail = event.target.email.value;
        let formPhoneNubmer = event.target.phoneNumber.value;
        let formAddress = event.target.address.value;
		let companyResponse = await fetch(`${apiUrl}/api/company`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            }
        });
		if (companyResponse.ok) {
			const companyData = await companyResponse.json();

			const companyId = companyData.id;

        let raw = JSON.stringify({
            "firstName": formFirstName,
            "lastName": formLastName,
            "email": formEmail,
            "phoneNumber": formPhoneNubmer,
            "contactAddress": formAddress,
            "companyId": companyId
        });

        const response = await fetch(`${apiUrl}/api/client`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: raw
        });
		if (response.status === 400){
			const errorJson = await response.json();
			setErrorMessage(errorJson.Message);
		} else {
			const clientData = await response.json();
			navigate('/client/' + clientData.id);
		}
		};
    }

    return (
        <div >
			{errorMessage && <div className="error-message">{errorMessage}</div>}
            <form onSubmit={ handleSubmit }>
                <label for="firstName">First Name: </label>
                <br/>
                <input type="text" id="firstName" name="firstName" />
                <br/>
                <label for="lastName">Last Name: </label>
                <br/>
                <input type="text" id="lastName" name="LastName" />
                <br/>
                <label for="email">Email: </label>
                <br/>
                <input type="email" id="email" name="email" />
                <br />
                <label for="phoneNumber">Phone Number: </label>
                <br />
                <input type="text" id="phoneNumber" name="phoneNumber" />
                <br />
                <label for="address">Address: </label>
                <br />
                <input type="text" id="address" name="address" />
                <br />
                <Button type="submit">Submit</Button>
            </form>
        </div>
    );
};