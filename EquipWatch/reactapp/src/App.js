import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Route, Routes } from 'react-router-dom';
import CompanyMainPage from './pages/companyMain/CompanyMain';
import EmployeeMainPage from './pages/employeeMain/EmployeeMain';
import ClientsPage from './pages/clients/ClientsPage';
import './App.css';

function App() {
    return (
        <Routes>
            <Route path="/" element={<EmployeeMainPage />} />
            <Route path="/company" element={<CompanyMainPage />} />
            <Route path="/clients" element={<ClientsPage />} />
        </Routes>
    );
}

export default App;
