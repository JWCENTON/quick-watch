import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navigation from './components/Navigation';
import Sidebar from './components/Sidebar';
import HomePage from './pages/HomePage';

const App = () => {
    return (
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
