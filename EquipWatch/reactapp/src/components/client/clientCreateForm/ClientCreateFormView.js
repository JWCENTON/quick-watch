import React from 'react';
import { Rating } from '@mui/material';
import { useState } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import './clientCreateForm.css';

export default function ClientCreateFormView() {
    const navigate = useNavigate();

    async function handleSubmit(event) {
        event.preventDefault();
        let formFirstName = event.target.firstName.value;
        let formLastName = event.target.lastName.value;
        let formEmail = event.target.email.value;
        let formPhoneNubmer = event.target.phoneNumber.value;
        let formAddress = event.target.address.value;

        let raw = JSON.stringify({
            "firstName": formFirstName,
            "lastName": formLastName,
            "email": formEmail,
            "phoneNumber": formPhoneNubmer,
            "contactAddress": formAddress,
            "companyId": "08db8c4f-47e7-4a22-8216-e1e16e7e9958"
        });

        const response = await fetch('https://localhost:7007/api/client', {
            method: "POST", 
            headers: { "Content-Type": "application/json"},
            body: raw
        });

        navigate("/clients");
    }

    return (
        <div >
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