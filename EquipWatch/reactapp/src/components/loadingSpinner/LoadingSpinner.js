import React, { useContext } from 'react';
import { Spinner } from 'react-bootstrap';
import { useLoading } from '../../contexts/LoadingContext';

const LoadingSpinner = () => {
    const { loading } = useLoading();

    return loading ? (
        <div className="global-loading-spinner">
            <Spinner animation="border" />
        </div>
    ) : null;
};

export default LoadingSpinner;
