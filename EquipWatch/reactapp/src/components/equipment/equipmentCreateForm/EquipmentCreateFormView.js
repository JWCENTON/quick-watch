import React from 'react';
import { Rating } from '@mui/material';
import { useState } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import './equipmentCreateForm.css';
import { useAuth } from '../../authProvider/AuthContext';

export default function EquipmentCreateFormView() {
    const navigate = useNavigate();
    const [rating, setRating] = useState(0);
    const { token } = useAuth();

    async function handleSubmit(event) {
        event.preventDefault();
        let formSerialNumber = event.target.serialNumber.value;
        let formCategory = event.target.category.value;
        let formLocation = event.target.location.value;

        let raw = JSON.stringify({
            "serialNumber": formSerialNumber,
            "category": formCategory,
            "location": formLocation,
            "condition": rating,
            "companyId": "08db8d19-9af1-4672-8929-920221b5bace"
        });

        const response = await fetch('https://localhost:7007/api/equipment', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: raw
        });

        navigate("/equipment");
    }

    return (
        <div >
            <form onSubmit={ handleSubmit }>
                <label for="serialNumber">Serial Number: </label>
                <br/>
                <input type="text" id="serialNumber" name="serialNumber" />
                <br/>
                <label for="category">Category: </label>
                <br/>
                <input type="text" id="category" name="category" />
                <br/>
                <label for="location">Location: </label>
                <br/>
                <input type="text" id="location" name="location" />
                <br />
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