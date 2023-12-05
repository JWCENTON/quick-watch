import React from 'react';
import { createRoot } from 'react-dom/client';
import ContextProviders from './contexts/ContextProviders';
import LoadingSpinner from './components/loadingSpinner/LoadingSpinner';
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import App from './App';

const root = document.getElementById('root');
createRoot(root).render(
    <React.StrictMode>
        <ContextProviders>
            <App />
            <LoadingSpinner />
        </ContextProviders>
    </React.StrictMode>
);
