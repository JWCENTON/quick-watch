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
        console.log("Form Submitted");
        let formSerialNumber = event.target.serialNumber.value;
        let formCategory = event.target.category.value;
        let formLocation = event.target.category.value;
        let formCondition = event.target.rating.value;

        const response = await fetch('https://localhost:7007/api/equipment', {
            method: "POST", body: {
                "serialNumber": formSerialNumber,
                "category": formCategory,
                "location": formLocation,
                "condition": formCondition,
                "company": {
                    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
                }
            }
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