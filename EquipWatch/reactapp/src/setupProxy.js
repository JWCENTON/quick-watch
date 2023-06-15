const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/weatherforecast",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7007',
        secure: false,
        headers: {
            Connection: 'Keep-Alive'
        }
    });

    app.use(appProxy);
};
