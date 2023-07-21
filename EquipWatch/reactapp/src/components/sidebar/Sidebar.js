import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import { SidebarContext } from '../../contexts/SidebarContext';
import MobileViewSearchbar from '../searchbar/MobileViewSearchbar';
import './Sidebar.css';

const Sidebar = () => {
    const { isSidebarOpen } = useContext(SidebarContext);
    
    const userRole = 'User';

    return (
        <div className={`sidebar ${isSidebarOpen ? 'open' : ''}`}>
            <div className="sidebar-searchbar">
                <MobileViewSearchbar inSidebar={true} />
            </div>
            <ul>
                <li>
                    <Button as={Link} to="/main" variant="outline-primary">
                        {userRole === 'Admin' ? 'Manage Company' : 'Check-In/Check-Out'}
                    </Button>
                </li>
                <li>
                    <Button as={Link} to="/commissions" variant="outline-primary">Commissions</Button>
                </li>
                <li>
                    <Button as={Link} to="/equipment" variant="outline-primary">Equipment</Button>
                </li>
                <li>
                    <Button as={Link} to="/clients" variant="outline-primary">Clients</Button>
                </li>
                {userRole === 'Admin' && (
                    <li>
                        <Button variant="outline-primary">Employees</Button>
                    </li>
                )}
                <li>
                    <Button as={Link} to="/personal-info" variant="outline-primary">Personal Info</Button>
                </li>
            </ul>
        </div>
    );
};

export default Sidebar;