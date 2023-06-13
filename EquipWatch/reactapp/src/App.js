import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Navigation from './components/navigation/Navigation';
import Sidebar from './components/sidebar/Sidebar';
import IndexCompanyView from './components/mainPage/IndexWithCompanyView';
import IndexEmployeeView from './components/mainPage/IndexWithEmployeeView';
import './App.css';
import CompanyCard from './components/companyCard/CompanyCard';

export default function App() {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <IndexEmployeeView />
            </div>
        </div>
    );
}
