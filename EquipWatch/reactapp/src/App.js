import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navigation from './components/navigation/Navigation';
import Sidebar from './components/sidebar/Sidebar';
import IndexEmployeeView from './components/mainPage/IndexWithEmployeeView';
import './App.css';

export default function App() {
    return (
        <div className="app-container">
            <Navigation />
            <div className="main-container">
                <Sidebar />
                <IndexEmployeeView />
            </div>
        </div>
    );
}
