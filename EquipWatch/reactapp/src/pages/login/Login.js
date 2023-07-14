import React, {useState} from 'react';
import './Login.css';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import placeholderImage from '../../images/placeholder.png';

function Login() {
    const location = useLocation();
    const registrationSuccess = location.state?.registrationSuccess;
    const username = location.state?.username;
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();

        const response = await fetch('https://localhost:7007/api/User/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                email,
                password,
            }),
        });

        if (response.ok) {
            // User login successful
            navigate('/employee');
        } else {
            const errorData = await response.json();
            if (errorData.title === 'EmailNotConfirmed') {
                setErrorMessage('Email not confirmed. Please check your email and confirm your account.');
            } else {
                setErrorMessage('Invalid email or password');
            }
        }
    };

    return (
        <div className="login-page">
            <div className="login-left">
                <img className="login-image" src={placeholderImage} alt="Placeholder" />
            </div>
            <div className="login-right">
                <h2>Welcome to EquipWatch</h2>
                {registrationSuccess && <p className="success-message">User: {username} has been successfully registered</p>}
                {errorMessage && <p className="error-message">{errorMessage}</p>}
                <form onSubmit={handleLogin}>
                    <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                    <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
                    <Button type="submit" variant="outline-primary">Login</Button>
                    <Button as={Link} to="/register" variant="outline-primary">Register</Button>
                </form>
            </div>
        </div>
    );
}

export default Login;