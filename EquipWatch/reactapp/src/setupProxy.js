const { createProxyMiddleware } = require('http-proxy-middleware');
const axios = require('axios');
const apiUrl = process.env.REACT_APP_API_URL;

const apiAxios = axios.create({
    baseURL: apiUrl,
    headers: {
        Accept: 'application/json',
    },
    secure: false
});

apiAxios.interceptors.request.use((config) => {
    const accessToken = localStorage.getItem('token');
    if (accessToken) {
        config.headers.common = { Authorization: `Bearer ${accessToken}` };
    }
    return config;
});

module.exports = function (app) {
    const apiProxy = createProxyMiddleware({
        target: `${apiUrl}`,
        secure: false,
        headers: {
            Connection: 'Keep-Alive'
        },
        onProxyReq: (proxyReq, req, res) => {
            if (req.path === '/' || req.path === '/register') {
                return;
            }
        },
        agent: apiAxios.defaults.httpAgent,
    });

    app.use(apiProxy);
};

module.exports = function (app) {
    const appProxy = createProxyMiddleware("/client", {
        target: `${apiUrl}/api/client`,
        secure: false,
        headers: {
            Connection: 'Keep-Alive'
        },
        agent: apiAxios.defaults.httpAgent,
    });

    app.use(appProxy);
};