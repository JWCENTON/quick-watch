import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Layout from '../../components/layout/Layout';
import ClientDetail from '../../components/client/ClientDetail';
import EquipmentDetail from '../../components/equipment/EquipmentDetail';
import CompanyDetail from '../../components/company/CompanyDetail';
import EmployeeDetail from '../../components/employee/EmployeeDetail';
import CommissionDetail from '../../components/commission/CommissionDetail';
import { useAuth } from '../../components/authProvider/AuthContext';
import './Detail.css';

export default function Detail() {
    const apiUrl = process.env.REACT_APP_API_URL;
    const { id, dataType } = useParams();
    const { token } = useAuth();

    const [detailsData, setDetailsData] = useState(null);

    const components = {
        client: ClientDetail,
        equipment: EquipmentDetail,
        commission: CommissionDetail,
        company: CompanyDetail,
        employee: EmployeeDetail
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
    }, [id, dataType, token, apiUrl]);

    return (
        <Layout>
            <ViewComponent detailsData={detailsData} />
        </Layout>
    );
};