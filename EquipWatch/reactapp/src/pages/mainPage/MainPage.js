import React, { useContext } from 'react';
import { SidebarContext } from '../../contexts/SidebarContext';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';
import IndexCompanyView from '../../components/mainPage/IndexWithCompanyView';
import IndexEmployeeView from '../../components/mainPage/IndexWithEmployeeView';

const MainPage = () => {
    const { toggleSidebar } = useContext(SidebarContext);
    
    const userRole = 'User';

    return (
        <div className="app-container">
            <Navigation onMenuClick={toggleSidebar} />
            <div className="main-container">
                <Sidebar />
                {userRole === 'Admin' ? <IndexCompanyView className="main-content" /> : <IndexEmployeeView className="main-content" />}
            </div>
        </div>
    );
};

export default MainPage;