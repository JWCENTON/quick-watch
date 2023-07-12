import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'react-bootstrap';

function Registration() {
    const navigate = useNavigate();
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        // Perform registration logic (e.g., send data to the server)
        // Assuming a successful registration, navigate back to the main page
        navigate('/');
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        const response = await fetch('https://localhost:7007/api/User/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                firstName,
                lastName,
                email,
                password,
            }),
        });

        if (response.ok) {
            // User registration successful
            // You can handle the success case here, such as showing a success message or redirecting to a different page
            console.log('Registration successful');
        } else {
            // User registration failed
            // You can handle the error case here, such as displaying the error message to the user
            const errorData = await response.json();
            console.log('Registration failed:', errorData);
        }
    };

    return (
        <div>
            <h2>Registration</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="First Name"
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Last Name"
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}
                />
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <Button onClick={handleRegister} type="submit" variant="outline-primary">
                    Register
                </Button>
            </form>
        </div>
    );
}

export default Registration;
