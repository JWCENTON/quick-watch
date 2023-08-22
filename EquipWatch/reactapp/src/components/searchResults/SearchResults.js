import React from 'react';
import { useLocation } from 'react-router-dom';

const SearchResults = () => {
    const location = useLocation();
    const results = location.state?.results || [];

    return (
        <div>
            <h1>Search Results:</h1>
            <pre>{JSON.stringify(results, null, 2)}</pre>
        </div>
    );
};

export default SearchResults;