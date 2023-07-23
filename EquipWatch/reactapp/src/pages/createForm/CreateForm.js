import React from 'react';
import { useParams } from 'react-router-dom';
import Layout from '../../components/layout/Layout';
import ClientCreateFormView from '../../components/client/clientCreateForm/ClientCreateFormView';
import EquipmentCreateFormView from '../../components/equipment/equipmentCreateForm/EquipmentCreateFormView';
//import EmployeeCreateFormView from '../../components/employee/employeeCreateForm/EmployeeCreateFormView';
//import CommissionCreateFormView from '../../components/commission/commissionCreateForm/CommissionCreateFormView';
//import CompanyCreateFormView from '../../components/company/companyCreateForm/CompanyCreateFormView';

const CreateForm = () => {
    const { formType } = useParams();

    const componentMap = {
        'client': ClientCreateFormView,
        'equipment': EquipmentCreateFormView,
        //'employee': EmployeeCreateFormView,
        //'commission': CommissionCreateFormView,
        //'company': CompanyCreateFormView,
    }

    const FormView = componentMap[formType];

    return (
        <Layout>
            <FormView />
        </Layout>
    );
};

export default CreateForm;