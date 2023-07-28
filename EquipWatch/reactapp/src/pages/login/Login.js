import React, {useState, useContext} from 'react';
import './Login.css';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import placeholderImage from '../../images/placeholder.png';
import { AuthContext } from '../../components/authProvider/AuthContext'

function Login() {
    const location = useLocation();
    const registrationSuccess = location.state?.registrationSuccess;
    const username = location.state?.username;
    const { login } = useContext(AuthContext);
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();

        try {
            await login(email, password);
            navigate('/main');
        } catch (error) {
            if (error.message === 'EmailNotConfirmed') {
                setErrorMessage('Email is not confirmed. Please check your email and confirm your account.');
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
                    <Button as={Link} to="/forgotpassword" variant="outline-primary">Forgot Password</Button>
                </form>
            </div>
        </div>
    );
}

export default Login;