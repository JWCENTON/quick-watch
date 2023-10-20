const { createProxyMiddleware } = require('http-proxy-middleware');
const apiUrl = process.env.REACT_APP_API_URL;

module.exports = function (app) {
    app.use(
        createProxyMiddleware("/api", {
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
        })
    );

    app.use(
        createProxyMiddleware("/client", {
            target: `${apiUrl}/api/client`,
            secure: false,
            headers: {
                Connection: 'Keep-Alive'
            }
        })
    );
};