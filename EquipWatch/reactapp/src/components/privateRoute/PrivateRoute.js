import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../authProvider/AuthContext';

const PrivateRoute = () => {
    const { user } = useAuth(); 

    const isAuthenticated = !!user;

    return isAuthenticated ? <Outlet /> : <Navigate to="/" />;
};

export default PrivateRoute;
