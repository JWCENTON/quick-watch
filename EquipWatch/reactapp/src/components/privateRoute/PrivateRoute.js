import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../authProvider/AuthContext';

const PrivateRoute = () => {
    const { token } = useAuth(); 

    const isAuthenticated = !!token;

    return isAuthenticated ? <Outlet /> : <Navigate to="/" />;
};

export default PrivateRoute;
