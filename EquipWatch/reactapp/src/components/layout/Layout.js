import React from 'react';
import { useContext } from 'react';
import { SidebarContext } from '../../contexts/SidebarContext';
import Navigation from '../../components/navigation/Navigation';
import Sidebar from '../../components/sidebar/Sidebar';

const Layout = ({ children }) => {
    const { toggleSidebar } = useContext(SidebarContext);

    return (
        <div className="app-container">
            <Navigation onMenuClick={toggleSidebar} />
            <div className="main-container">
                <Sidebar />
                <div className="main-content">
                    {children}
                </div>
            </div>
        </div>
    );
};

export default Layout;