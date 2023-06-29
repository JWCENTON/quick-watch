import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import EmployeeDetailView from '../../components/employee/EmployeeDetailView';

const EmployeeDetail = () => {
    const { id } = useParams();
    const [employeeData, setEmployeeData] = useState(null);

    useEffect(() => {
        const fetchEmployeeData = async () => {
            const data = await fetch(`/api/employees/${id}`);
            setEmployeeData(data);
        };

        fetchEmployeeData();
    }, [id]);

    if (!employeeData) {
        return <div>Loading...</div>;
    }

    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <EmployeeDetailView employeeData={employeeData} />
            </div>
        </div>
    );
};

export default EmployeeDetail;