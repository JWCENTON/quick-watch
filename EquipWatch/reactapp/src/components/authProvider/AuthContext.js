import React, { createContext, useState, useContext } from 'react';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(localStorage.getItem('token') || null);
    const apiUrl = process.env.REACT_APP_API_URL;

    const login = async (email, password) => {
        const response = await fetch(`${apiUrl}/api/User/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email,
                password,
            }),
        });

        if (response.ok) {
            const { token } = await response.json();
            localStorage.setItem('token', token);
            setToken(token);
            setUser({ email });
        } else {
            const errorData = await response.json();
            throw new Error(errorData.title);
        }
    };

    const logout = () => {
        localStorage.removeItem('token');
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, token, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
