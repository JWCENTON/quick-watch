import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import CompanyEditView from '../../components/company/CompanyEditView';

const CompanyEdit = () => {
    const { id } = useParams();
    const [companyData, setCompanyData] = useState(null);

    useEffect(() => {
        const fetchCompanyData = async () => {
            const data = await fetch(`/api/companies/${id}`);
            setCompanyData(data);
        };

        fetchCompanyData();
    }, [id]);

    if (!companyData) {
        return <div>Loading...</div>;
    }

    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <CompanyEditView companyData={companyData} />
            </div>
        </div>
    );
};

export default CompanyEdit;