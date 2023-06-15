import React from 'react';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import IndexCompanyView from '../../components/mainPage/IndexWithCompanyView';

const CompanyMainPage = () => {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <IndexCompanyView />
            </div>
        </div>
    );
};

export default CompanyMainPage;