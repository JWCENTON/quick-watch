import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Button } from 'react-bootstrap';

function ResetPassword() {
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const userId = searchParams.get('userId');
    const token = searchParams.get('token');
    const [newPassword, setNewPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState('');
    const [error, setError] = useState('');

    const handleResetPassword = async (e) => {
        e.preventDefault();
        try {
            setLoading(true);
            setError('');

            const response = await fetch('https://localhost:7007/api/User/resetPassword', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    userId,
                    token,
                    newPassword,
                }),
            });

            if (response.ok) {
                setMessage('Password reset successful');
            } else {
                const errorData = await response.json();
                setError(errorData.message || 'An error occurred. Please try again later.');
            }

            setLoading(false);
        } catch (error) {
            setLoading(false);
            setError('An error occurred. Please try again later.');
        }
    };

    return (
        <div>
            <h2>Reset Password</h2>
            <form onSubmit={handleResetPassword}>
                <input
                    type="password"
                    placeholder="New Password"
                    value={newPassword}
                    onChange={(e) => setNewPassword(e.target.value)}
                />
                <Button type="submit" variant="outline-primary" disabled={loading}>
                    Reset Password
                </Button>
            </form>
            {message && <p className="success-message">{message}</p>}
            {error && <p className="error-message">{error}</p>}
        </div>
    );
}

export default ResetPassword;
