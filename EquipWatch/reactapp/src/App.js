import React from 'react';
import { Routes, Route } from 'react-router-dom';
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
import './App.css';

const createPrivateRoute = (path, Component) => (
    <Route path={path} element={<PrivateRoute />}>
        <Route index element={<Component />} />
    </Route>
);

function App() {
    return (
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/register" element={<Registration />} />
            <Route path="/forgotpassword" element={<ForgotPassword />} />
            <Route path="/resetPassword/:userId/:token" element={<ResetPassword />} />

            {createPrivateRoute("/main", Main)}
            {createPrivateRoute("/companies", CardList)}
            {createPrivateRoute("/clients", CardList)}
            {createPrivateRoute("/equipment", CardList)}
            {createPrivateRoute("/commissions", CardList)}
            {createPrivateRoute("/:formType/create", CreateForm)}
            {createPrivateRoute("/personal-info", PersonalInfo)}
            {createPrivateRoute("/:dataType/:id", Detail)}
            {createPrivateRoute("/:dataType/:id/edit", Edit)}
            {createPrivateRoute("/search", CardList)}

            <Route path="*" element={<Error />} />
        </Routes>
    );
}

export default App;