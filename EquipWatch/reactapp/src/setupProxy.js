const { createProxyMiddleware } = require('http-proxy-middleware');


module.exports = function (app) {
    const apiProxy = createProxyMiddleware({
        target: 'https://localhost:7007',
        secure: false,
        headers: {
            Connection: 'Keep-Alive'
        },
        onProxyReq: (proxyReq, req, res) => {
            if (req.path === '/' || req.path === '/register') {
                return;
            }
        },
    });

    app.use(apiProxy);
};

module.exports = function (app) {
    const appProxy = createProxyMiddleware("/client", {
        target: 'https://localhost:7007/api/client',
        secure: false,
        headers: {
            Connection: 'Keep-Alive'
        }
    });

    app.use(appProxy);
};