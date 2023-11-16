import React, { createContext, useState, useContext } from 'react';
import axios from 'axios';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const apiUrl = process.env.REACT_APP_API_URL;
    const token = localStorage.getItem('token');

    const authAxios = axios.create({
        baseURL: apiUrl,
        headers: {
            'Content-Type':'application/json',
        }
    });

    authAxios.interceptors.request.use((config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config
    },
    error => {
        Promise.reject(error.response || error.message);
        }
    );

    const login = async (email, password) => {
        try {
            const response = await authAxios.post('/api/User/login', { email, password });
            const { token } = response.data;
            localStorage.setItem('token', token);
            setUser({email});
        }
        catch (error) {
            throw new Error(error.response.data.title);
        }
    };

    const logout = () => {
        localStorage.removeItem('token');
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, login, logout, token, authAxios }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
