import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import { SidebarProvider } from './SidebarContext';
import { LoadingProvider } from './LoadingContext';
import { AuthProvider } from './authProvider/AuthContext';

const ContextProviders = ({ children }) => {
    return (
        <AuthProvider>
            <SidebarProvider>
                <BrowserRouter>
                    <LoadingProvider>
                        {children}
                    </LoadingProvider>
                </BrowserRouter>
            </SidebarProvider>
        </AuthProvider>
    );
};

export default ContextProviders;