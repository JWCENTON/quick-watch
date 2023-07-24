import React from 'react';
import { Rating } from '@mui/material';
import { useState } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import './equipmentCreateForm.css';

export default function EquipmentCreateFormView() {
    const navigate = useNavigate();
    const [rating, setRating] = useState(0);

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