import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CompanyMainPage from './pages/CompanyMain/CompanyMain';
import EmployeeMainPage from './pages/EmployeeMain/EmployeeMain';
import PersonalInfoPage from './pages/personalInfo/PersonalInfo';
import ClientDetail from './pages/client/ClientDetail';
import ClientEdit from './pages/client/ClientEdit';
import CardList from './pages/cardList/CardListPage';
import Login from './pages/login/Login';
import './App.css';

function App() {
    return (
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/employee" element={<EmployeeMainPage />} />
                <Route path="/company" element={<CompanyMainPage />} />
                <Route path="/companies" element={<CardList />} />
                <Route path="/clients" element={<CardList />} />
                <Route path="/equipment" element={<CardList />} />
                <Route path="/commissions" element={<CardList />} />
                <Route path="/personal-info" element={<PersonalInfoPage />} />
                <Route path="/client/:id" component={<ClientDetail />} />
                <Route path="/client/:id/edit" component={<ClientEdit />} />
            </Routes>
    );
}

export default App;
