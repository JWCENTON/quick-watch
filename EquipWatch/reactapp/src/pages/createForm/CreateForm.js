import React from 'react';
import { useParams } from 'react-router-dom';
import Layout from '../../components/layout/Layout';
import ClientCreateForm from '../../components/client/clientCreateForm/ClientCreateForm';
import EquipmentCreateForm from '../../components/equipment/equipmentCreateForm/EquipmentCreateForm';
//import EmployeeCreateForm from '../../components/employee/employeeCreateForm/EmployeeCreateForm';
import CommissionCreateForm from '../../components/commission/commissionCreateForm/CommissionCreateForm';
//import CompanyCreateForm from '../../components/company/companyCreateForm/CompanyCreateForm';

const CreateForm = () => {
    const { formType } = useParams();

    const componentMap = {
        'client': ClientCreateForm,
        'equipment': EquipmentCreateForm,
        //'employee': EmployeeCreateForm,
        'commission': CommissionCreateForm,
        //'company': CompanyCreateForm,
    }

    const FormView = componentMap[formType];

    return (
        <Layout>
            <FormView />
        </Layout>
    );
};

export default CreateForm;