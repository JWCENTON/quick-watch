import React, { useState} from 'react';
import './Login.css';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import placeholderImage from '../../images/placeholder.png';

function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleRegister = async (e) => {
        e.preventDefault();
        const response = await fetch('https://localhost:7007/api/User/register', {
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
        <div className="login-page">
            <div className="login-left">
                <img className="login-image" src={placeholderImage} alt="Placeholder" />
            </div>
            <div className="login-right">
                <h2>Welcome to EquipWatch</h2>
                <form>
                    <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                    <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
                    <Button as={Link} to="/main" variant="outline-primary">Login</Button>
                    <Button onClick={handleRegister} variant="outline-primary">Register</Button>
                </form>
            </div>
        </div>
    );
}

export default Login;