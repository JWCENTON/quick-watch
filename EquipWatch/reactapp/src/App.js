import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import IndexCompanyView from './components/mainPage/IndexWithCompanyView';
import IndexEmployeeView from './components/mainPage/IndexWithEmployeeView';
import CompanyCard from './components/companyCard/CompanyCard';

export default function App() {
    return (
        //<div>Hello</div>
        <>
            <CompanyCard name="TestName" address="Warsaw" createdAt="12.07.2022"/>
            <br/>
            <CompanyCard name="TestName" address="Warsaw" createdAt="13.07.2022" />
            <br />
            <CompanyCard name="TestName" address="Warsaw" createdAt="14.07.2022" />
            <br />
            <CompanyCard name="TestName" address="Warsaw" createdAt="15.07.2022" />
            <br />
            <IndexCompanyView />
            <br />
            <br />
            <IndexEmployeeView />
        </>

    );
}
