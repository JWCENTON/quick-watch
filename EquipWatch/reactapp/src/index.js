import React from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { SidebarProvider } from './contexts/SidebarContext';
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import App from './App';
import Registration from './pages/registration/Registration';
import Error from './pages/error/Error';
import Main from './pages/main/Main';
import CardList from './pages/cardList/CardList';
import PersonalInfoPage from './pages/personalInfo/PersonalInfo';
import Detail from './pages/detail/Detail';
import Edit from './pages/edit/Edit';
import CreateForm from './pages/createForm/CreateForm';
import ForgotPassword from './pages/passwordReset/ForgotPassword';
import ResetPassword from './components/resetPassword/ResetPassword';


const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <Error />,
        children: [
            { path: "main", element: <Main /> },
            { path: "register", element: <Registration /> },
            { path: "forgotpassword", element: <ForgotPassword /> },
            { path: "resetPassword/:userId/:token", element: <ResetPassword /> },
            { path: "clients", element: <CardList /> },
            { path: "companies", element: <CardList /> },
            { path: "equipment", element: <CardList /> },
            { path: "commissions", element: <CardList /> },
            { path: "personal-info", element: <PersonalInfoPage /> },
            { path: ":dataType/:id", element: <Detail /> },
            { path: ":dataType/:id/edit", element: <Edit /> },
            { path: "search", element: <CardList /> },
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