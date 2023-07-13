import React from 'react';
import './Login.css';
import { Link, useLocation } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import placeholderImage from '../../images/placeholder.png';

function Login() {
    const location = useLocation();
    const registrationSuccess = location.state?.registrationSuccess;
    const username = location.state?.username;

    return (
        <div className="login-page">
            <div className="login-left">
                <img className="login-image" src={placeholderImage} alt="Placeholder" />
            </div>
            <div className="login-right">
                <h2>Welcome to EquipWatch</h2>
                {registrationSuccess && <p className="success-message">User: {username} has been successfully registered</p>}
                <form>
                    <input type="email" placeholder="Email" />
                    <input type="password" placeholder="Password" />
                    <Button as={Link} to="/employee" variant="outline-primary">Login</Button>
                    <Button as={Link} to="/register" variant="outline-primary">Register</Button>
                </form>
            </div>
        </div>
    );
}

export default Login;