import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navigation from './components/Navigation';
import Sidebar from './components/Sidebar';
import HomePage from './pages/HomePage';
import IndexCompanyView from "./components/Main/IndexWithCompanyView";
import IndexEmployeeView from './components/Main/IndexWithEmployeeView';

export default function App() {
    return (
        <div>Hello</div>
            <Router>
            <Navigation />
            <Sidebar />
            <Routes>
                <Route path='/' element={<HomePage />} />
            </Routes>
        </Router>
    );
};

export default App;
