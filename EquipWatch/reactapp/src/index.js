// index.js
import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { SidebarProvider } from './contexts/SidebarContext';
import { LoadingProvider } from './contexts/LoadingContext';
import LoadingSpinner from './components/loadingSpinner/LoadingSpinner';
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import App from './App';

const root = document.getElementById('root');
createRoot(root).render(
    <React.StrictMode>
        <SidebarProvider>
            <BrowserRouter>
                <LoadingProvider>
                    <App />
                    <LoadingSpinner />
                </LoadingProvider>
            </BrowserRouter>
        </SidebarProvider>
    </React.StrictMode>
);
