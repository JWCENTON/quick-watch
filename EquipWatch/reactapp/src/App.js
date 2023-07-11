import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CompanyMainPage from './pages/companyMain/CompanyMain';
import EmployeeMainPage from './pages/employeeMain/EmployeeMain';
import PersonalInfoPage from './pages/personalInfo/PersonalInfo';
import DetailView from './pages/detailView/DetailView';
import EditView from './pages/editView/EditView';
import CardList from './pages/cardList/CardListPage';
import Login from './pages/login/Login';
import EquipmentCreateForm from './pages/equipmentCreateForm/EquipmentCreateForm';
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
            <Route path="/equipment/create" element={<EquipmentCreateForm />} />
            <Route path="/commissions" element={<CardList />} />
            <Route path="/personal-info" element={<PersonalInfoPage />} />
            <Route path="/:dataType/:id" element={<DetailView />} />
            <Route path="/:dataType/:id/edit" element={<EditView />} />
        </Routes>
    );
}

export default App;