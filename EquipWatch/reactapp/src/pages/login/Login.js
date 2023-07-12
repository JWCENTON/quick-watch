import React from 'react';
import './Login.css';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import placeholderImage from '../../images/placeholder.png';

function Login() {
    return (
        <div className="login-page">
            <div className="login-left">
                <img className="login-image" src={placeholderImage} alt="Placeholder" />
            </div>
            <div className="login-right">
                <h2>Welcome to EquipWatch</h2>
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