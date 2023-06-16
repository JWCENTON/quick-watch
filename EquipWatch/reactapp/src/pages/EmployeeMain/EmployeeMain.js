import React from 'react';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import IndexEmployeeView from '../../components/mainPage/IndexWithEmployeeView';

const EmployeeMainPage = () => {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <IndexEmployeeView />
            </div>
        </div>
    );
};

export default EmployeeMainPage;