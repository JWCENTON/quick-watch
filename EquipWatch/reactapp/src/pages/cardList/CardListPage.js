import React from 'react';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import CardList from '../../components/cardList/CardList';

const ClientsPage = () => {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <CardList />
            </div>
        </div>
    );
};

export default ClientsPage;