import React from 'react';
import { useState } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import './clientCreateForm.css';
import { useAuth } from '../../authProvider/AuthContext';

export default function ClientCreateForm() {
    const navigate = useNavigate();
    const { authAxios } = useAuth();
	const [errorMessage, setErrorMessage] = useState('');

    async function handleSubmit(event) {
        event.preventDefault();
        let formFirstName = event.target.firstName.value;
        let formLastName = event.target.lastName.value;
        let formEmail = event.target.email.value;
        let formPhoneNubmer = event.target.phoneNumber.value;
        let formAddress = event.target.address.value;
        let companyResponse = await await authAxios.get('/api/company'); 

        if (companyResponse.status === 200) {
            const companyData = await companyResponse.data;
            const companyId = companyData.id;

            const requestData = {
                firstName: formFirstName,
                lastName: formLastName,
                email: formEmail,
                phoneNumber: formPhoneNubmer,
                contactAddress: formAddress,
                companyId: companyId,
            };
            const response = await authAxios.post('/api/client', requestData);

            if (response.status === 200) {
                const clientData = response.data;
                navigate('/client/' + clientData.id);
            } else if (response.status === 400) {
                const errorJson = await response.json();
                setErrorMessage(errorJson.Message);
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