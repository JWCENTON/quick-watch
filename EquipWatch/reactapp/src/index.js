import React from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { SidebarProvider } from './contexts/SidebarContext';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';
import './index.css';
import ErrorPage from './pages/error/error-page';
import MainPage from './pages/mainPage/MainPage';
import CardList from './pages/cardList/CardListPage';
import PersonalInfoPage from './pages/personalInfo/PersonalInfo';
import DetailView from './pages/detailView/DetailView';
import EditView from './pages/editView/EditView';

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <ErrorPage />,
        children: [
            { path: "main", element: <MainPage /> },
            { path: "clients", element: <CardList /> },
            { path: "companies", element: <CardList /> },
            { path: "equipment", element: <CardList /> },
            { path: "commissions", element: <CardList /> },
            { path: "personal-info", element: <PersonalInfoPage /> },
            { path: ":dataType/:id", element: <DetailView /> },
            { path: ":dataType/:id/edit", element: <EditView /> }
        ],
    }
]);

const root = document.getElementById('root');
createRoot(root).render(
    <React.StrictMode>
        <SidebarProvider>
            <RouterProvider router={router} />
        </ SidebarProvider>
    </React.StrictMode>
);