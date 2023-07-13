import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../authProvider/AuthContext'; // Import your authentication context

const PrivateRoute = () => {
    const { user } = useAuth(); // Access the authenticated user from the context

    // Check if the user is authenticated
    const isAuthenticated = !!user;

    // If authenticated, render the child elements
    // If not, navigate to the login page
    return isAuthenticated ? <Outlet /> : <Navigate to="/" />;
};

export default PrivateRoute;
