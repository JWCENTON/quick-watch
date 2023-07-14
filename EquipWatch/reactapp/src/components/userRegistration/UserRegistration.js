import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import './UserRegistration.css';

function Registration() {
    const navigate = useNavigate();
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState([]);
    const [isLoading, setLoading] = useState(false);


    const handleSubmit = async (e) => {
        e.preventDefault();
        navigate('/');
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        setLoading(true);

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
            console.log('Registration successful');
            const username = `${firstName} ${lastName}`;
            navigate('/', { state: { registrationSuccess: true, username } });
        } else {
            try {
                const errorData = await response.json();
                if (Array.isArray(errorData)) {
                    const errorMessages = errorData.map((error) => `${error.code}: ${error.description}`);
                    setErrorMessage(['Registration failed:', ...errorMessages]);
                } else if (errorData.message) {
                    setErrorMessage(['Registration failed:', errorData.message]);
                } else {
                    setErrorMessage(['Registration failed: An error occurred.']);
                }
            } catch (error) {
                console.log('Error parsing error response:', error);
                setErrorMessage(['Registration failed: An error occurred.']);
            }
        }

        setLoading(false);
    };

    return (
        <div>
            <h2>Registration</h2>
            {errorMessage.length > 0 && (
                <div className="error-message">
                    {errorMessage.length > 1 && <p>{errorMessage[0]}</p>}
                    <ul>
                        {errorMessage.slice(1).map((message, index) => (
                            <li key={index}>{message}</li>
                        ))}
                    </ul>
                </div>
            )}
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
                <Button onClick={handleRegister} type="submit" variant="outline-primary" disabled={isLoading}>
                    Register
                </Button>
            </form>
        </div>
    );
}

export default Registration;
