import React from 'react';
import { useState } from 'react';
import { Button, Form} from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import 'react-date-range/dist/styles.css';
import 'react-date-range/dist/theme/default.css';
import './commissionCreateForm.css';
import { DateRangePicker } from 'react-date-range';

export default function CommissionCreateFormView() {
    const navigate = useNavigate();
    const [dateRange, setDateRange] = useState([
        {
            startDate: null,
            endDate: null,
            key: "selection"
        }
    ]);

    async function handleSubmit(event) {
        event.preventDefault();
        let formClient = event.target.client.value;
        let formLocation = event.target.location.value;
        let formDescription = event.target.description.value;
        let formScope = event.target.scope.value;

        let raw = JSON.stringify({
            "clientId": formClient,
            "location": formLocation,
            "description": formDescription,
            "scope": formScope,
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
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <Form.Label for="client">Client: </Form.Label>
                    <Form.Select id="client" aria-label="Select Client">
                    </Form.Select>
                </Form.Group>
                <Form.Group>
                    <Form.Label for="location">Location: </Form.Label>
                    <Form.Control type="text" id="location" name="location" placeholder="Location" />
                </Form.Group>
                <Form.Group>
                <Form.Label for="description">Description: </Form.Label>
                    <Form.Control as="textarea" id="description" name="description" rows={5} placeholder="Description" />
                </Form.Group>
                <Form.Group>
                    <Form.Label for="scope">Scope: </Form.Label>
                    <Form.Control type="text" id="scope" name="scope" placeholder="Scope" />
                </Form.Group>
                <Form.Group>
                    <Form.Label for="dates">Dates: </Form.Label>
                    <DateRangePicker
                        ranges={dateRange}
                        onChange={item => setDateRange([item.selection])}
                        minDate={new Date()}
                        />
                </Form.Group>
                <Form.Group>
                    <Button type="submit">Submit</Button>
                </Form.Group>
            </Form>  
        </div>
    );
};