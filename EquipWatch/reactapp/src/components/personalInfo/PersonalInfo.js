import React, { useState, useEffect } from 'react';
import './PersonalInfo.css';
import { Button } from 'react-bootstrap';
import { useAuth } from '../../contexts/authProvider/AuthContext';

function PersonalInfo() {
    const { authAxios } = useAuth();
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
            const response = await authAxios.get('/api/User/getUserInfo');

            if (response.status === 200) {
                const data = response.data;
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
            const response = await updateUserInfo();

            if (response.status === 200) {
                setEditing(false);
            } else {
                displayErrorMessage('Failed to update user information');
            }
        } catch (error) {
            displayErrorMessage('Failed to update user information', error);
        }
    };

    const updateUserInfo = async () => {
        return await authAxios.put('/api/User/updateUserInfo', userInfo);
    };

    const handlePasswordChange = async (e) => {
        e.preventDefault();
        try {
            const response = await changePassword();

            if (response.status === 200) {
                clearPasswordFields();
                displaySuccessMessage('Password has been changed successfully');
            } else {
                displayErrorMessage('Failed to change password');
            }
        } catch (error) {
            displayErrorMessage('Failed to change password', error);
        }
    };

    const changePassword = async () => {
        return await authAxios.post('/api/User/changePassword', passwordData);
    };

    const clearPasswordFields = () => {
        setPasswordData({
            currentPassword: '',
            newPassword: '',
        });
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

    const renderEditForm = () => (
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
    );

    const renderUserInfo = () => (
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
    );

    return (
        <div className="background-color wrapper">
            <h3>Personal Information</h3>
            <br />
            {editing ? (
                renderEditForm()
            ) : (
                renderUserInfo()
            )}
            {error && <p className="error-message">{error}</p>}
            {successMsg && <p className="success-message">{successMsg}</p>}
        </div>
    );
}

export default PersonalInfo;
