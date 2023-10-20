import React, { useContext } from 'react';
import { Spinner } from 'react-bootstrap';
import { LoadingContext } from '../contexts/LoadingContext';

const LoadingSpinner = () => {
    const { loading } = useContext(LoadingContext);

    return (
        loading && (
            <div className="loading-spinner">
                <Spinner animation="border" />
            </div>
        )
    );
};

export default LoadingSpinner;
