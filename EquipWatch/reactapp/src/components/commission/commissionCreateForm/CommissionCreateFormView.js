import React from 'react';
import { useState, useEffect } from 'react';
import { Button, Form} from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import 'react-date-range/dist/styles.css';
import 'react-date-range/dist/theme/default.css';
import './commissionCreateForm.css';
import { DateRangePicker } from 'react-date-range';
import { useAuth } from '../../authProvider/AuthContext';

export default function CommissionCreateFormView() {
    const navigate = useNavigate();
    const [options, setOptions] = useState(null);
    const { token } = useAuth(); 
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
            "companyId": "08db8dad-76cc-4174-87e4-c529682dd54c", 
            "startTime": dateRange[0].startDate,
            "endTime": dateRange[0].endDate
        });

        const response = await fetch('https://localhost:7007/api/commission', {
            method: "POST", 
            headers: { "Content-Type": "application/json"},
            body: raw
        });

        navigate("/commissions");
    }
    async function GetData(token) {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        const response = await fetch('https://localhost:7007/api/client', { headers });
        const data = await response.json();
        setOptions(data);
    }

    useEffect(() => { GetData(token) }, [])

    return (
        <div >
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <Form.Label for="client">Client: </Form.Label>
                    <Form.Select id="client" aria-label="Select Client">
                        {options == null ? <option disabled>Loading...</option> : options.map((client, index) => (<option key={index} value={client.id}>{client.firstName} {client.lastName}</option>))}
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