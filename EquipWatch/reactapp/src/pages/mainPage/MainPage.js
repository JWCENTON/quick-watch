import React from 'react';
import Layout from '../../components/layout/Layout';
import IndexCompanyView from '../../components/mainPage/IndexWithCompanyView';
import IndexEmployeeView from '../../components/mainPage/IndexWithEmployeeView';

const MainPage = () => {
    const userRole = 'User';

    return (
        <Layout>
            {userRole === 'Admin' ? <IndexCompanyView /> : <IndexEmployeeView />}
        </Layout>
    );
};

export default MainPage;