import React, { useState } from 'react';
import { Button } from 'react-bootstrap';
import { Rating } from '@mui/material';
import { useNavigate } from "react-router-dom";
import './equipmentCreateForm.css';
import { useAuth } from '../../../contexts/authProvider/AuthContext';

export default function EquipmentCreateForm() {
    const navigate = useNavigate();
    const { authAxios } = useAuth();
    const [rating, setRating] = useState(0);
    const [errorMessage, setErrorMessage] = useState('');

    async function handleSubmit(event) {
        event.preventDefault();
        const formSerialNumber = event.target.serialNumber.value;
        const formCategory = event.target.category.value;
        const formLocation = event.target.location.value;
        const companyResponse = await authAxios.get('/api/company');
        if (companyResponse.status === 200) {
            const companyData = companyResponse.data;
            const companyId = companyData.id;

            let raw = JSON.stringify({
                "serialNumber": formSerialNumber,
                "category": formCategory,
                "location": formLocation,
                "condition": rating,
                "companyId": companyId
            });

            const response = await authAxios.post('/api/equipment', raw, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (response.status === 200) {
                const equipmentData = response.data;
                navigate("/equipment/" + equipmentData.id);
            } else if (response.status === 400) {
                const errorJson = await response.data;
                setErrorMessage(errorJson.Message);
            }
        }
    }

    return (
        <div>
            {errorMessage && <div className="error-message">{errorMessage}</div>}
            <form onSubmit={handleSubmit}>
                <label htmlFor="category">Category: </label>
                <br />
                <input type="text" id="category" name="category" />
                <br />
                <label htmlFor="location">Location: </label>
                <br />
                <input type="text" id="location" name="location" />
                <br />
                <label htmlFor="serialNumber">Serial Number: </label>
                <br />
                <input type="text" id="serialNumber" name="serialNumber" />
                <br />
                <label htmlFor="rating">Condition: </label>
                <br />
                <Rating
                    id="rating"
                    name="rating"
                    value={rating}
                    onChange={(event, newValue) => {
                        setRating(newValue);
                    }}
                />
                <br />
                <Button type="submit">Submit</Button>
            </form>
        </div>
    );
}