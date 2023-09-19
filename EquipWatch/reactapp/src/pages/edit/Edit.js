import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Layout from '../../components/layout/Layout';
import ClientEdit from '../../components/client/ClientEdit';
import EquipmentEdit from '../../components/equipment/EquipmentEdit';
import CommissionEdit from '../../components/commission/CommissionEdit';
import CompanyEdit from '../../components/company/CompanyEdit';
import EmployeeEdit from '../../components/employee/EmployeeEdit';
import { useAuth } from '../../components/authProvider/AuthContext';

export default function Edit() {
    const apiUrl = process.env.REACT_APP_API_URL;
    const { id, dataType } = useParams();
    const { token } = useAuth();

    const [detailsData, setDetailsData] = useState(null);

    const components = {
        client: ClientEdit,
        equipment: EquipmentEdit,
        commission: CommissionEdit,
        company: CompanyEdit,
        employee: EmployeeEdit
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