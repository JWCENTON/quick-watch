import React from 'react';
import IndexCompanyView from "./components/Main/IndexWithCompanyView";
import IndexEmployeeView from './components/Main/IndexWithEmployeeView';

export default function App() { 
    return (
        <>
            <IndexCompanyView />
            <br/>
            <IndexEmployeeView />
        </>
        
    );
}
