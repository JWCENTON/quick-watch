import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import EquipmentEditView from '../../components/equipment/EquipmentEditView';

const EquipmentEdit = () => {
    const { id } = useParams();
    const [equipmentData, setEquipmentData] = useState(null);

    useEffect(() => {
        const fetchEquipmentData = async () => {
            const data = await fetch(`/api/equipment/${id}`);
            setEquipmentData(data);
        };

        fetchEquipmentData();
    }, [id]);

    if (!equipmentData) {
        return <div>Loading...</div>;
    }

    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <EquipmentEditView equipmentData={equipmentData} />
            </div>
        </div>
    );
};

export default EquipmentEdit;