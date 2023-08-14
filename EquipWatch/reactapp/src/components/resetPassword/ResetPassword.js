import React, { useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';

function ResetPassword() {
    const { userId, token } = useParams();
    const [newPassword, setNewPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState('');
    const [error, setError] = useState('');

    const handleResetPassword = async (e) => {
        e.preventDefault();
        console.log(`token ${token} and userId ${userId}`);
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
                    token: encodeURIComponent(token),
                    newPassword,
                    PasswordResetToken: encodeURIComponent(token),
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
            {message ? (
                <div>
                    <h2>{message}</h2>
                    <Link to="/">Go to Login Page</Link>
                </div>
            ) : (
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
                    {error && <p className="error-message">{error}</p>}
                </div>
            )}
        </div>
    );
}

export default ResetPassword;
