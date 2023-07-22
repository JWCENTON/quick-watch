import React from 'react';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import EquipmentCreateFormView from '../../components/equipment/equipmentCreateForm/EquipmentCreateFormView';

const EquipmentCreateForm = () => {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <EquipmentCreateFormView />
            </div>
        </div>
    );
};

export default EquipmentCreateForm;