import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CompanyMainPage from './pages/CompanyMain/CompanyMain';
import EmployeeMainPage from './pages/EmployeeMain/EmployeeMain';
import CardList from './pages/cardList/CardListPage';
import './App.css';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<EmployeeMainPage />} />
                <Route path="/company" element={<CompanyMainPage />} />
                <Route path="/clients" element={<CardList />} />
                <Route path="/equipment" element={<CardList />} />
                <Route path="/commissions" element={<CardList />} />
            </Routes>
        </Router>
    );
}

export default App;
