import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';

function ForgotPassword() {
    const [email, setEmail] = useState('');

    const handleForgotPassword = async (e) => {
        e.preventDefault();

    };

    return (
        <div className="forgot-password-page">
            <h2>Forgot Password</h2>
            <form onSubmit={handleForgotPassword}>
                <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                <Button type="submit" variant="outline-primary">Send Reset Link</Button>
            </form>
            <Link to="/">Back to Login</Link>
        </div>
    );
}

export default ForgotPassword;
