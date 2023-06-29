import React, { useEffect, useState } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import ClientDetailView from '../../components/client/ClientDetailView';
import EquipmentDetailView from '../../components/equipment/EquipmentDetailView';
import CompanyDetailView from '../../components/company/CompanyDetailView';
import EmployeeDetailView from '../../components/employee/EmployeeDetailView';
import CommissionDetailView from '../../components/commission/CommissionDetailView';

export default function DetailView() {
    const { id, dataType } = useParams();

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
            const response = await fetch(`https://localhost:7007/api/${dataType}/${id}`);
            const data = await response.json();
            setDetailsData(data);
        };
        fetchDetailsData();
    }, [id]);

    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <ViewComponent detailsData={detailsData}></ViewComponent>
            </div>
        </div>
    );
};


