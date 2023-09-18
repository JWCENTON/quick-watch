import React from 'react';
import Layout from '../../components/layout/Layout';
import IndexCompany from '../../components/mainPage/IndexWithCompany';
import IndexEmployee from '../../components/mainPage/IndexWithEmployee';

const Main = () => {
    const userRole = 'User';

    return (
        <Layout>
            {userRole === 'Admin' ? <IndexCompany /> : <IndexEmployee />}
        </Layout>
    );
};

export default Main;