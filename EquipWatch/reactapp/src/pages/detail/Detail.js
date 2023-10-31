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
    const { id, dataType } = useParams();
    const auth = useAuth(); 

    const [detailsData, setDetailsData] = useState(null);

    const components = {
        client: ClientDetail,
        equipment: EquipmentDetail,
        commission: CommissionDetail,
        company: CompanyDetail,
        employee: EmployeeDetail,
    };

    const ViewComponent = components[dataType];

    useEffect(() => {
        const fetchDetailsData = async () => {
            const response = await auth.authAxios.get(`/api/${dataType}/${id}`); 
            const data = response.data;
            setDetailsData(data);
        };
        fetchDetailsData();
    }, [id, dataType, auth]);

    return (
        <Layout>
            <ViewComponent detailsData={detailsData} />
        </Layout>
    );
}
