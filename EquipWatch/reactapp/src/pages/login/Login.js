import React, { useState, useContext } from 'react';
import './Login.css';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { Button, Spinner } from 'react-bootstrap';
import placeholderImage from '../../images/placeholder.png';
import { AuthContext } from '../../contexts/authProvider/AuthContext';

function Login() {
    const location = useLocation();
    const registrationSuccess = location.state?.registrationSuccess;
    const username = location.state?.username;
    const auth = useContext(AuthContext);
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const handleLogin = async (e) => {
        e.preventDefault();
        setIsLoading(true);

        try {
            await auth.login(email, password);
            navigate('/commissions');
        } catch (error) {
            if (error.message === 'EmailNotConfirmed') {
                setErrorMessage('Email is not confirmed. Please check your email and confirm your account.');
            } else {
                setErrorMessage('Invalid email or password');
            }
        } finally {
            setIsLoading(false);
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
                    <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} disabled={isLoading} />
                    <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} disabled={isLoading} />
                    <Button type="submit" variant="outline-primary" disabled={isLoading}>
                        {isLoading ? <Spinner as="span" animation="border" size="sm" role="status" aria-hidden="true" /> : "Login"}
                    </Button>
                    <Button as={Link} to="/register" variant="outline-primary">
                        Register
                    </Button>
                    <Button as={Link} to="/forgotpassword" variant="outline-primary">
                        Forgot Password
                    </Button>
                </form>
            </div>
        </div>
    );
}

export default Login;
