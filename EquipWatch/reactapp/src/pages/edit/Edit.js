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
    const { id, dataType } = useParams();
    const auth = useAuth();

    const [detailsData, setDetailsData] = useState(null);

    const components = {
        client: ClientEdit,
        equipment: EquipmentEdit,
        commission: CommissionEdit,
        company: CompanyEdit,
        employee: EmployeeEdit,
    };

    const ViewComponent = components[dataType];

    useEffect(() => {
        const fetchDetailsData = async () => {
            const response = await auth.authAxios.get(`/api/${dataType}/${id}`); 
            const data = response.data;
            setDetailsData(data);
        };
        fetchDetailsData();
    }, [id, auth]);

    return (
        <Layout>
            <ViewComponent detailsData={detailsData} />
        </Layout>
    );
}
