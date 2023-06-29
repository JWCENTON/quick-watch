import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import ClientDetailView from '../../components/client/ClientDetailView';

const ClientDetail = () => {
    const { id } = useParams();
    
    const [clientData, setClientData] = useState(null);
    
    useEffect(() => {
        const fetchClientData = async () => {
            const data = await fetch(`/api/clients/${id}`);
            setClientData(data);
        };

        fetchClientData();
    }, [id]);
    
    if (!clientData) {
        return <div>Loading...</div>;
    }

    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <ClientDetailView clientData={clientData} />
            </div>
        </div>
    );
};

export default ClientDetail;