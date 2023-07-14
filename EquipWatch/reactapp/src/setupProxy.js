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



const appProxy = createProxyMiddleware("/", {
    target: 'https://localhost:7007',
    secure: false,
    headers: {
        Connection: 'Keep-Alive'
    }
});

module.exports = function (app) {
    app.use(appProxy);
};

module.exports = function (app) {
    const appProxy = createProxyMiddleware("client", {
        target: 'https://localhost:7007/api/client',
        secure: false,
        headers: {
            Connection: 'Keep-Alive'
        }
    });

    app.use(appProxy);
};