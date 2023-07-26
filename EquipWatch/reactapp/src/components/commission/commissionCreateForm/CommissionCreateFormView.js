import React from 'react';
import { Button, Form} from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import './commissionCreateForm.css';

export default function CommissionCreateFormView() {
    const navigate = useNavigate();

    async function handleSubmit(event) {
        event.preventDefault();
        let formSerialNumber = event.target.serialNumber.value;
        let formCategory = event.target.category.value;
        let formLocation = event.target.location.value;

        let raw = JSON.stringify({
            "serialNumber": formSerialNumber,
            "category": formCategory,
            "location": formLocation,
            "companyId": "08db8c4f-47e7-4a22-8216-e1e16e7e9958"
            
        });

        const response = await fetch('https://localhost:7007/api/equipment', {
            method: "POST", 
            headers: { "Content-Type": "application/json"},
            body: raw
        });

        navigate("/equipment");
    }

    return (
        <div >
            <Form onSubmit={ handleSubmit }>
                <Form.Label for="client">Client: </Form.Label>
                <Form.Select id="client" aria-label="Select Client">
                </Form.Select>
                <Form.Label for="location">Location: </Form.Label>
                <Form.Control type="text" id="location" name="location" placeholder="Location" />
                <Form.Label for="description">Description: </Form.Label>
                <Form.Control as="textarea" id="description" name="description" rows={5} />
                <Button type="submit">Submit</Button>
            </Form>
        </div>
    );
};