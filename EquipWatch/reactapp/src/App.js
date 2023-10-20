import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { AuthProvider } from './components/authProvider/AuthContext';
import { SidebarProvider } from './contexts/SidebarContext';
import Login from './pages/login/Login';
import PrivateRoute from './components/privateRoute/PrivateRoute';
import Registration from './pages/registration/Registration';
import ForgotPassword from './pages/passwordReset/ForgotPassword';
import ResetPassword from './components/resetPassword/ResetPassword';
import Error from './pages/error/Error';
import Main from './pages/main/Main';
import CardList from './pages/cardList/CardList';
import PersonalInfo from './pages/personalInfo/PersonalInfo';
import Detail from './pages/detail/Detail';
import Edit from './pages/edit/Edit';
import CreateForm from './pages/createForm/CreateForm';
import 'bootstrap/dist/css/bootstrap.min.css';
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
                        <Route index element={<Main />} />
                    </Route>
                    <Route path="/companies" element={<PrivateRoute />}>
                        <Route index element={<CardList />} />
                    </Route>
                    <Route path="/clients" element={<PrivateRoute />}>
                        <Route index element={<CardList />} />
                    </Route>
                    <Route path="/equipment" element={<PrivateRoute />}>
                        <Route index element={<CardList />} />
                    </Route>
                    <Route path="/commissions" element={<PrivateRoute />}>
                        <Route index element={<CardList />} />
                    </Route>
                    <Route path="/:formType/create" element={<PrivateRoute />}>
                        <Route index element={<CreateForm />} />
                    </Route>
                    <Route path="/personal-info" element={<PrivateRoute />}>
                        <Route index element={<PersonalInfo />} />
                    </Route>
                    <Route path="/:dataType/:id" element={<PrivateRoute />}>
                        <Route index element={<Detail />} />
                    </Route>
                    <Route path="/:dataType/:id/edit" element={<PrivateRoute />}>
                        <Route index element={<Edit />} />
                    </Route>
                    <Route path="/search" element={<PrivateRoute />}>
                        <Route index element={<CardList />} />
                    </Route>
                    <Route path="*" element={<Error />} />
                </Routes>
            </SidebarProvider>
        </AuthProvider>
    );
}

export default App;