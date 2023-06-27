import React from 'react';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import PersonalInfo from '../../components/personalInfo/PersonalInfo';

const PersonalInfoPage = () => {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <PersonalInfo />
            </div>
        </div>
    );
};

export default PersonalInfoPage;