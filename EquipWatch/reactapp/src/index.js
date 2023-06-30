import React from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';
import './index.css';
import ErrorPage from './pages/error/error-page';
import CompanyMainPage from './pages/companyMain/CompanyMain';
import CardList from './pages/cardList/CardListPage';
import EmployeeMainPage from './pages/employeeMain/EmployeeMain';
import PersonalInfoPage from './pages/personalInfo/PersonalInfo';
import DetailView from './pages/detailView/DetailView';
import EditView from './pages/editView/EditView';

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <ErrorPage />,
        children: [
            { path: "employee", element: <EmployeeMainPage /> },
            { path: "company", element: <CompanyMainPage /> },
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
        <RouterProvider router={router} />
    </React.StrictMode>
);