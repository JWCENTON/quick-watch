import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Routes, Route } from 'react-router-dom';
import { SidebarProvider } from './contexts/SidebarContext';
import MainPage from './pages/mainPage/MainPage';
import DetailView from './pages/detailView/DetailView';
import EditView from './pages/editView/EditView';
import CardList from './pages/cardList/CardListPage';
import Login from './pages/login/Login';
import './App.css';

function App() {
    return (
        <SidebarProvider>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/main" element={<MainPage />} />
                <Route path="/companies" element={<CardList />} />
                <Route path="/clients" element={<CardList />} />
                <Route path="/equipment" element={<CardList />} />
                <Route path="/commissions" element={<CardList />} />
                <Route path="/personal-info" element={<MainPage />} />
                <Route path="/:dataType/:id" element={<DetailView />} />
                <Route path="/:dataType/:id/edit" element={<EditView />} />
            </Routes>
        </SidebarProvider>
    );
}

export default App;