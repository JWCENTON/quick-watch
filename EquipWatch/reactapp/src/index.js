import React from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';
import './index.css';
import ErrorPage from './pages/error/error-page';
import CompanyMainPage from './pages/companyMain/CompanyMain';
import ClientsPage from './pages/clients/ClientsPage';

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <ErrorPage />,
        children: [
            { path: "company", element: <CompanyMainPage /> },
            { path: "clients", element: <ClientsPage /> }
        ],
    }
]);

const root = document.getElementById('root');
createRoot(root).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);