import axios from 'axios';
import { useContext } from 'react';
import { LoadingContext } from '../contexts/LoadingContext';

const useApi = () => {
    const { setLoading } = useContext(LoadingContext);

    const api = axios.create({
        baseURL: process.env.REACT_APP_API_URL,
    });

    api.interceptors.request.use(
        (config) => {
            setLoading(true);
            return config;
        },
        (error) => {
            setLoading(false);
            return Promise.reject(error);
        }
    );

    api.interceptors.response.use(
        (response) => {
            setLoading(false);
            return response;
        },
        (error) => {
            setLoading(false);
            return Promise.reject(error);
        }
    );

    return { api };
};

export default useApi;
