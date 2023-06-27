import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CompanyMainPage from './pages/CompanyMain/CompanyMain';
import EmployeeMainPage from './pages/EmployeeMain/EmployeeMain';
import PersonalInfoPage from './pages/personalInfo/PersonalInfo';
import CardList from './pages/cardList/CardListPage';
import Login from './pages/login/Login';
import './App.css';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/employee" element={<EmployeeMainPage />} />
                <Route path="/company" element={<CompanyMainPage />} />
                <Route path="/companies" element={<CardList />} />
                <Route path="/clients" element={<CardList />} />
                <Route path="/equipment" element={<CardList />} />
                <Route path="/commissions" element={<CardList />} />
                <Route path="/personalInfo" element={<PersonalInfoPage />} />
            </Routes>
        </Router>
    );
}

export default App;
