import React from 'react';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import Clients from '../../components/clients/clients';

const ClientsPage = () => {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <Clients />
            </div>
        </div>
    );
};

export default ClientsPage;