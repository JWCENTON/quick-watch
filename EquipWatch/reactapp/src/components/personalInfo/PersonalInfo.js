import React, { useState, useEffect } from 'react';
import './PersonalInfo.css';
import { Button } from 'react-bootstrap';

function PersonalInfo() {
    const [userInfo, setUserInfo] = useState({
        firstName: '',
        lastName: '',
        email: '',
    });
    const [editing, setEditing] = useState(false);
    const [passwordData, setPasswordData] = useState({
        currentPassword: '',
        newPassword: '',
    });
    const [error, setError] = useState('');
    const [successMsg, setSuccessMsg] = useState('');

    const clearMessages = () => {
        setSuccessMsg('');
        setError('');
    };

    const displaySuccessMessage = (message) => {
        setSuccessMsg(message);
        setTimeout(clearMessages, 5000);
    };

    const displayErrorMessage = (message) => {
        setError(message);
        setTimeout(clearMessages, 5000);
    };

    useEffect(() => {
        fetchUserInfo();
    }, []);

    const fetchUserInfo = async () => {
        try {
            const response = await fetch('https://localhost:7007/api/User/getUserInfo', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            });

            if (response.ok) {
                const data = await response.json();
                setUserInfo(data);
            } else {
                displayErrorMessage('Failed to fetch user information');
            }
        } catch (error) {
            displayErrorMessage('Failed to fetch user information', error);
        }
    };

    const handleEdit = () => {
        setEditing(true);
    };

    const handleSave = async () => {
        try {
            const response = await fetch('https://localhost:7007/api/User/updateUserInfo', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
                body: JSON.stringify(userInfo),
            });

            if (response.ok) {
                setEditing(false);
            } else {
                displayErrorMessage('Failed to update user information');
            }
        } catch (error) {
            displayErrorMessage('Failed to update user information', error);
        }
    };

    const handlePasswordChange = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('https://localhost:7007/api/User/changePassword', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
                body: JSON.stringify(passwordData),
            });

            if (response.ok) {
                setPasswordData({
                    currentPassword: '',
                    newPassword: '',
                });
                displaySuccessMessage('Password has been changed successfully');
            } else {
                displayErrorMessage('Failed to change password');
            }
        } catch (error) {
            displayErrorMessage('Failed to change password', error);
        }
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setUserInfo((prevUserInfo) => ({
            ...prevUserInfo,
            [name]: value,
        }));
    };

    const handlePasswordInputChange = (e) => {
        const { name, value } = e.target;
        setPasswordData((prevPasswordData) => ({
            ...prevPasswordData,
            [name]: value,
        }));
    };

    return (
        <div className="background-color wrapper">
            <h3>Personal Information</h3>
            <br />
            {editing ? (
                <div>
                    <label htmlFor="firstName">First Name:</label>
                    <input
                        type="text"
                        id="firstName"
                        name="firstName"
                        value={userInfo.firstName}
                        onChange={handleInputChange}
                    />
                    <br />
                    <label htmlFor="lastName">Last Name:</label>
                    <input
                        type="text"
                        id="lastName"
                        name="lastName"
                        value={userInfo.lastName}
                        onChange={handleInputChange}
                    />
                    <br />
                    <label htmlFor="email">Contact Email:</label>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        value={userInfo.email}
                        onChange={handleInputChange}
                    />
                    <br />
                    <Button onClick={handleSave} variant="primary">
                        Save
                    </Button>
                </div>
            ) : (
                <div>
                    <p>
                        <strong>First Name:</strong> {userInfo.firstName}
                    </p>
                    <p>
                        <strong>Last Name:</strong> {userInfo.lastName}
                    </p>
                    <p>
                        <strong>Contact Email:</strong> {userInfo.email}
                    </p>
                    <Button onClick={handleEdit} variant="secondary">
                        Edit
                    </Button>
                    <form onSubmit={handlePasswordChange}>
                        <label htmlFor="currentPassword">Current Password:</label>
                        <input
                            type="password"
                            id="currentPassword"
                            name="currentPassword"
                            value={passwordData.currentPassword}
                            onChange={handlePasswordInputChange}
                        />
                        <br />
                        <label htmlFor="newPassword">New Password:</label>
                        <input
                            type="password"
                            id="newPassword"
                            name="newPassword"
                            value={passwordData.newPassword}
                            onChange={handlePasswordInputChange}
                        />
                        <br />
                        <Button type="submit" variant="danger">
                            Change Password
                        </Button>
                    </form>
                </div>
            )}
            {error && <p className="error-message">{error}</p>}
            {successMsg && <p className="success-message">{successMsg}</p>}
        </div>
    );
}

export default PersonalInfo;