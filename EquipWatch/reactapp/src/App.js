import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Routes, Route } from 'react-router-dom';
import { AuthProvider } from './components/authProvider/AuthContext';
import { SidebarProvider } from './contexts/SidebarContext';
import Login from './pages/login/Login';
import PrivateRoute from './components/privateRoute/PrivateRoute';
import MainPage from './pages/mainPage/MainPage';
import CardList from './pages/cardList/CardListPage';
import PersonalInfoPage from './pages/personalInfo/PersonalInfo';
import DetailView from './pages/detailView/DetailView';
import EditView from './pages/editView/EditView';
import CreateForm from './pages/createForm/CreateForm';
import Registration from './components/userRegistration/UserRegistration';
import ErrorPage from './pages/error/error-page';
import ForgotPassword from './components/forgotPassword/ForgotPassword';
import ResetPassword from './components/resetPassword/ResetPassword';
import './App.css';

function App() {
    return (
        <AuthProvider>
            <SidebarProvider>
                <Routes>
                    <Route path="/" element={<Login />} />
                    <Route path="/register" element={<Registration />} />
                    <Route path="/forgotpassword" element={<ForgotPassword />} />
                    <Route path="/resetPassword/:userId/:token" element={<ResetPassword />} />

                    <Route path="/main" element={<PrivateRoute />}>
                        <Route path="/main" element={<MainPage />} />    
                    </Route>
                    <Route path="/companies" element={<PrivateRoute />}>
                        <Route path="/companies" element={<CardList />} />
                    </Route>
                    <Route path="/clients" element={<PrivateRoute />}>
                        <Route path="/clients" element={<CardList />} />
                    </Route>
                    <Route path="/equipment" element={<PrivateRoute />}>
                        <Route path="/equipment" element={<CardList />} />
                    </Route>
                    <Route path="/commissions" element={<PrivateRoute />}>
                        <Route path="/commissions" element={<CardList />} />
                    </Route>
                    <Route path="/:formType/create" element={<PrivateRoute />}>
                        <Route path="/:formType/create" element={<CreateForm />} />
                    </Route>
                    <Route path="/personal-info" element={<PrivateRoute />}>
                        <Route path="/personal-info" element={<PersonalInfoPage />} />
                    </Route>
                    <Route path="/:dataType/:id" element={<PrivateRoute />}>
                        <Route path="/:dataType/:id" element={<DetailView />} />
                    </Route>
                    <Route path="/:dataType/:id/edit" element={<PrivateRoute />}>
                        <Route path="/:dataType/:id/edit" element={<EditView />} />
                    </Route>
                    <Route path="*" element={<ErrorPage />} />
                </Routes>
            </SidebarProvider>
        </AuthProvider>
    );
}

export default App;
