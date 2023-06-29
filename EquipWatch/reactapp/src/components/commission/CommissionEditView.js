import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import CommissionEditView from '../../components/commission/CommissionEditView';

const CommissionEdit = () => {
    const { id } = useParams();
    const [commissionData, setCommissionData] = useState(null);

    useEffect(() => {
        const fetchCommissionData = async () => {
            const data = await fetch(`/api/commissions/${id}`);
            setCommissionData(data);
        };

        fetchCommissionData();
    }, [id]);

    if (!commissionData) {
        return <div>Loading...</div>;
    }

    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <CommissionEditView commissionData={commissionData} />
            </div>
        </div>
    );
};

export default CommissionEdit;