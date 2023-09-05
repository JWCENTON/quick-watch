import React from 'react';
import { Rating } from '@mui/material';
import { useState } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import './equipmentCreateForm.css';
import { useAuth } from '../../authProvider/AuthContext';

export default function EquipmentCreateFormView() {
    const apiUrl = process.env.REACT_APP_API_URL;
    const navigate = useNavigate();
    const [rating, setRating] = useState(0);
    const { token } = useAuth();
	const [errorMessage, setErrorMessage] = useState('');

    async function handleSubmit(event) {
        event.preventDefault();
        let formSerialNumber = event.target.serialNumber.value;
        let formCategory = event.target.category.value;
        let formLocation = event.target.location.value;
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
            "serialNumber": formSerialNumber,
            "category": formCategory,
            "location": formLocation,
            "condition": rating,
            "companyId": companyId
        });

        const response = await fetch(`${apiUrl}/api/equipment`, {
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
			const equipmentData = await response.json();
			navigate("/equipment/" + equipmentData.id);
		}
		};
    }

    return (
        <div >
			{errorMessage && <div className="error-message">{errorMessage}</div>}
            <form onSubmit={ handleSubmit }>
                <label for="category">Category: </label>
                <br/>
                <input type="text" id="category" name="category" />
                <br/>
                <label for="location">Location: </label>
                <br/>
                <input type="text" id="location" name="location" />
                <br />
				<label for="serialNumber">Serial Number: </label>
                <br/>
                <input type="text" id="serialNumber" name="serialNumber" />
                <br/>
                <label for="rating">Condition: </label>
                <br/>
                <Rating
                    id="rating"
                    name="rating"
                    value={rating}
                    onChange={(event, newValue) => {
                        setRating(newValue);
                    }}
                />
                <br/>
                <Button type="submit">Submit</Button>
            </form>
        </div>
    );
};