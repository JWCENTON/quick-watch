import React from 'react';
import { useState, useEffect } from 'react';
import { Button, Form } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import 'react-date-range/dist/styles.css';
import 'react-date-range/dist/theme/default.css';
import './commissionCreateForm.css';
import { DateRangePicker } from 'react-date-range';
import { useAuth } from '../../authProvider/AuthContext';

export default function CommissionCreateForm() {
    const apiUrl = process.env.REACT_APP_API_URL;
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
	const [errorMessage, setErrorMessage] = useState('');

    async function handleSubmit(event) {
        event.preventDefault();
        let formClient = event.target.client.value;
        let formLocation = event.target.location.value;
        let formDescription = event.target.description.value;
        let formScope = event.target.scope.value;
		
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
            "clientId": formClient,
            "location": formLocation,
            "description": formDescription,
            "scope": formScope,
            "companyId": companyId, 
            "startTime": dateRange[0].startDate,
            "endTime": dateRange[0].endDate
        });
		
        const response = await fetch(`${apiUrl}/api/commission`, {
            method: "POST", 
            headers: { "Content-Type": "application/json",
			'Authorization': `Bearer ${token}`},
            body: raw
        });

		if (response.status === 400){
			const errorJson = await response.json();
			setErrorMessage(errorJson.Message);
		} else {
			const commissionData = await response.json();
			navigate("/commission/" + commissionData.id);
		}
		};
    }
    async function GetData(token) {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        const response = await fetch(`${apiUrl}/api/client`, 
			{ headers: {'Authorization': `Bearer ${token}`}
			});
        const data = await response.json();
        setOptions(data);
    }

    useEffect(() => { GetData(token) }, [])

    return (
        <div >
			{errorMessage && <div className="error-message">{errorMessage}</div>}
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
                    <Form.Label for="dates">Dates: </Form.Label><br/>
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