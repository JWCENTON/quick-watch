import React from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { SidebarProvider } from './contexts/SidebarContext';
import { AuthProvider } from './components/authProvider/AuthContext'
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import App from './App';
import SearchResults from './components/searchResults/SearchResults';
import Registration from './pages/registration/Registration';
import ErrorPage from './pages/error/error-page';
import MainPage from './pages/mainPage/MainPage';
import CardList from './pages/cardList/CardListPage';
import PersonalInfoPage from './pages/personalInfo/PersonalInfo';
import DetailView from './pages/detailView/DetailView';
import EditView from './pages/editView/EditView';
import CreateForm from './pages/createForm/CreateForm';
import ForgotPasswordPage from './pages/passwordReset/ForgotPasswordPage';
import ResetPassword from './components/resetPassword/ResetPassword';


const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <ErrorPage />,
        children: [
            { path: "main", element: <MainPage /> },
            { path: "register", element: <Registration /> },
            { path: "forgotpassword", element: <ForgotPasswordPage /> },
            { path: "resetPassword/:userId/:token", element: <ResetPassword /> },
            { path: "clients", element: <CardList /> },
            { path: "companies", element: <CardList /> },
            { path: "equipment", element: <CardList /> },
            { path: "commissions", element: <CardList /> },
            { path: "personal-info", element: <PersonalInfoPage /> },
            { path: ":dataType/:id", element: <DetailView /> },
            { path: ":dataType/:id/edit", element: <EditView /> },
            { path: "search-results", element: <SearchResults /> },
            { path: "/:formType/create", element: <CreateForm /> }
        ]
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