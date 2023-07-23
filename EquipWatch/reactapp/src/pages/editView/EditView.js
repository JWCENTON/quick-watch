import React, { useEffect, useState, useContext } from 'react';
import { SidebarContext } from '../../contexts/SidebarContext';
import { useParams } from 'react-router-dom';
import Layout from '../../components/layout/Layout';
import ClientEditView from '../../components/client/ClientEditView';
import EquipmentEditView from '../../components/equipment/EquipmentEditView';
import CommissionEditView from '../../components/commission/CommissionEditView';
import CompanyEditView from '../../components/company/CompanyEditView';
import EmployeeEditView from '../../components/employee/EmployeeEditView';

export default function EditView() {
    const { id, dataType } = useParams();

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
            const response = await fetch(`https://localhost:7007/api/${dataType}/${id}`);
            const data = await response.json();
            setDetailsData(data);
        };
        fetchDetailsData();
    }, [id]);

    return (
        <Layout>
                <ViewComponent detailsData={detailsData} />
        </Layout >
    );
};