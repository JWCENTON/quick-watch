import React from 'react';
import { Rating } from '@mui/material';
import { useState } from 'react';
import { Button } from 'react-bootstrap';
import './equipmentCreateForm.css';

export default function EquipmentCreateFormView() {
    const [rating, setRating] = useState(0);

    return (
        <div >
            <form>
                <label>Serial Number: </label>
                <br/>
                <input type="text" id="serialNumber"/>
                <br/>
                <label>Category: </label>
                <br/>
                <input type="text" id="category"/>
                <br/>
                <label>Location: </label>
                <br/>
                <input type="text" id="location"/>
                <br />
                <label>Condition: </label>
                <br/>
                <Rating
                    id="rating"
                    name="simple-controlled"
                    value={rating}
                    onChange={(event, newValue) => {
                        setRating(newValue);
                    }}
                />
                <br/>
                <Button>Submit</Button>
            </form>
        </div>
    );
};