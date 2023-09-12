import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Layout from '../../components/layout/Layout';
import ClientDetailView from '../../components/client/ClientDetailView';
import EquipmentDetailView from '../../components/equipment/EquipmentDetailView';
import CompanyDetailView from '../../components/company/CompanyDetailView';
import EmployeeDetailView from '../../components/employee/EmployeeDetailView';
import CommissionDetailView from '../../components/commission/CommissionDetailView';
import { useAuth } from '../../components/authProvider/AuthContext';
import './DetailView.css';

export default function DetailView() {
    const apiUrl = process.env.REACT_APP_API_URL;
    const { id, dataType } = useParams();
    const { token } = useAuth();

    const [detailsData, setDetailsData] = useState(null);

    const components = {
        client: ClientDetailView,
        equipment: EquipmentDetailView,
        commission: CommissionDetailView,
        company: CompanyDetailView,
        employee: EmployeeDetailView
    };

    const ViewComponent = components[dataType];

    useEffect(() => {
        const fetchDetailsData = async () => {
            const response = await fetch(`${apiUrl}/api/${dataType}/${id}`, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            });
            const data = await response.json();
            setDetailsData(data);
        };
        fetchDetailsData();
    }, [id, dataType, token]);

    return (
        <Layout>
            <ViewComponent detailsData={detailsData} />
        </Layout>
    );
};