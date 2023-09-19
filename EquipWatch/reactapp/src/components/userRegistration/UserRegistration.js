import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import './UserRegistration.css';

function Registration() {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
    });
    const [errorMessage, setErrorMessage] = useState([]);
    const [isLoading, setLoading] = useState(false);
    const apiUrl = process.env.REACT_APP_API_URL;

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        navigate('/');
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        setLoading(true);

        const response = await fetch(`${apiUrl}/api/User/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(formData),
        });

        if (response.ok) {
            const username = `${formData.firstName} ${formData.lastName}`;
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
                    name="firstName"
                    value={formData.firstName}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    placeholder="Last Name"
                    name="lastName"
                    value={formData.lastName}
                    onChange={handleChange}
                />
                <input
                    type="email"
                    placeholder="Email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                />
                <input
                    type="password"
                    placeholder="Password"
                    name="password"
                    value={formData.password}
                    onChange={handleChange}
                />
                <Button onClick={handleRegister} type="submit" variant="outline-primary" disabled={isLoading}>
                    Register
                </Button>
            </form>
            <Link to="/">Back to Login</Link>
        </div>
    );
}

export default Registration;