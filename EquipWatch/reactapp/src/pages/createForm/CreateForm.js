import React from 'react';
import { useRouteMatch } from 'react-router-dom';
import Layout from '../../components/layout/Layout';
import ClientCreateFormView from '../../components/client/clientCreateForm/ClientCreateFormView';
import EquipmentCreateFormView from '../../components/client/equipmentCreateForm/EquipmentCreateFormView';
import EmployeeCreateFormView from '../../components/client/employeeCreateForm/EmployeeCreateFormView';
import CommissionCreateFormView from '../../components/client/commissionCreateForm/CommissionCreateFormView';
import CompanyCreateFormView from '../../components/client/companyCreateForm/CompanyCreateFormView';

const CreateForm = () => {
    const match = useRouteMatch();

    const componentMap = {
        '/client/create': ClientCreateFormView,
        '/equipment/create': EquipmentCreateFormView,
        //'/employee/create': EmployeeCreateFormView,
        //'/commission/create': CommissionCreateFormView,
        //'/company/create': CompanyCreateFormView
    }

    const FormView = componentMap[match.path];

    return (
        <Layout>
            <FormView />
        </Layout>
    );
};

export default CreateForm;