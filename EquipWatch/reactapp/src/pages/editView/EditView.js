import React, { useEffect, useState, useContext } from 'react';
import { SidebarContext } from '../../contexts/SidebarContext';
import { useParams } from 'react-router-dom';
import Layout from '../../components/layout/Layout';
import ClientEditView from '../../components/client/ClientEditView';
import EquipmentEditView from '../../components/equipment/EquipmentEditView';
import CommissionEditView from '../../components/commission/CommissionEditView';
import CompanyEditView from '../../components/company/CompanyEditView';
import EmployeeEditView from '../../components/employee/EmployeeEditView';
import { useAuth } from '../../components/authProvider/AuthContext';

export default function EditView() {
    const apiUrl = process.env.REACT_APP_API_URL;
    const { id, dataType } = useParams();
    const { token } = useAuth();

    const [detailsData, setDetailsData] = useState(null);

    const components = {
        client: ClientEditView,
        equipment: EquipmentEditView,
        commission: CommissionEditView,
        company: CompanyEditView,
        employee: EmployeeEditView
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
    }, [id, token]);

    return (
        <Layout>
            <ViewComponent detailsData={detailsData} />
        </Layout >
    );
};