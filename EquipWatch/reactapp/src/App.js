import React from 'react';
import EmployeeMainPage from './pages/EmployeeMain/EmployeeMain';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CompanyMainPage from './pages/CompanyMain/CompanyMain';
import './App.css';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<EmployeeMainPage />} />
                <Route path="/company" element={<CompanyMainPage />} />
            </Routes>
        </Router>
    );
}

export default App;
