import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import { useAuth } from '../../contexts/authProvider/AuthContext';

function ForgotPassword() {
    const [email, setEmail] = useState('');
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState('');
    const [error, setError] = useState('');
    const { authAxios } = useAuth();

    const handleForgotPassword = async (e) => {
        e.preventDefault();

        try {
            setLoading(true);
            setError('');

            const response = await authAxios.post(`/api/User/forgotPassword`, {
                email,
            });

            if (response.status === 200) {
                setMessage('Password reset link was sent');
            } else {
                setError(response.data.Message || 'An error occurred. Please try again later.');
            }

            setLoading(false);
        } catch (error) {
            setLoading(false);
            setError('An error occurred. Please try again later.');
        }

    };

    return (
        <div className="forgot-password-page">
            <h2>Forgot Password</h2>
            <form onSubmit={handleForgotPassword}>
                <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                <Button type="submit" variant="outline-primary" disabled={loading}>
                    Send Reset Link
                </Button>
            </form>
            {message && <p className="success-message">{message}</p>}
            {error && <p className="error-message">{error}</p>}
            <Link to="/">Back to Login</Link>
        </div>
    );
}

export default ForgotPassword;
