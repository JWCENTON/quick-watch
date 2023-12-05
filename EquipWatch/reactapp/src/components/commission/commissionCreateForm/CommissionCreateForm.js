import React from 'react';
import { useState, useEffect } from 'react';
import { Button, Form, Spinner } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import 'react-date-range/dist/styles.css';
import 'react-date-range/dist/theme/default.css';
import './commissionCreateForm.css';
import { DateRangePicker } from 'react-date-range';
import { useAuth } from '../../../contexts/authProvider/AuthContext';

export default function CommissionCreateForm() {
    const navigate = useNavigate();
    const [options, setOptions] = useState(null);
    const [isLoading, setIsLoading] = useState(false); 
    const { authAxios } = useAuth(); 
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
		
        const companyResponse = await authAxios.get('/api/company');
    
		if (companyResponse === 200) {
			const companyData = await companyResponse.data;
			const companyId = companyData.id;

        const requestData = {
            "clientId": formClient,
            "location": formLocation,
            "description": formDescription,
            "scope": formScope,
            "companyId": companyId, 
            "startTime": dateRange[0].startDate,
            "endTime": dateRange[0].endDate
        };
		
        const response = await authAxios.post('/api/commission', requestData);

		if (response.status === 200){
            const commissionData = response.data;
            navigate("/commission/" + commissionData.id);
		} else if (response.status === 400) {
            const errorJson = await response.data;
            setErrorMessage(errorJson.Message);
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
        const response = await authAxios.get('/api/client', { headers }); 
        const data = response.data;
        setOptions(data);
    }

    useEffect(() => {
        setIsLoading(true);
        GetData(authAxios.defaults.headers.authorization).finally(() => setIsLoading(false));
    }, []);

    return (
        <div >
			{errorMessage && <div className="error-message">{errorMessage}</div>}
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <Form.Label for="client">Client: </Form.Label>
                    <Form.Group>
                        <Form.Label htmlFor="client">Client: </Form.Label>
                        {isLoading ? (
                            <div className="text-center">
                                <Spinner animation="border" />
                            </div>
                        ) : (
                            <Form.Select id="client" aria-label="Select Client">
                                {options && options.map((client, index) => (
                                    <option key={index} value={client.id}>{client.firstName} {client.lastName}</option>
                                ))}
                            </Form.Select>
                        )}
                    </Form.Group>
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