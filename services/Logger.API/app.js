var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');
var apiUtils = require('./infrastructure/utils/apiUtils');
var ErrorLog = require('./models/ErrorLog');


var app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(
    express.urlencoded({
        extended: false
    })
);
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

//swagger utilization
const swaggerUi = require('swagger-ui-express');
const yaml = require('yamljs');
const swaggerDoc = yaml.load('./resources/swagger/Logger.API.v1.yaml');
app.use('/logger/swagger', swaggerUi.serve, swaggerUi.setup(swaggerDoc));

// routers
var errorLoggerRouter = require('./routes/errorLogger');
app.use('/logger/api/v1/error-log', errorLoggerRouter);
var performanceLoggerRouter = require('./routes/performanceLogger');
app.use('/logger/api/v1/performance-log', performanceLoggerRouter);


//global exception handlers
app.use((err, req, res, next) => {
    var errorLog = {};
    if (err instanceof apiUtils.ManagedError) {
        errorLog = {
            ...new ErrorLog(),
            ...err,
            ...{ stackTrace: err.stack }
        };

        res.status(400).json(errorLog);
    }
    else {
        errorLog = {
            ...new ErrorLog(),
            ...{
                message: err.message,
                type: err.name,
                stackTrace: err.stack,
                topic: 'UnhandledException',
                level: 'Error',
                title: 'An unexpected error occured'
            }
        };

        res.status(500).json(errorLog);
    }

    apiUtils.LogManagement.putErrorLogToQueue(errorLog);
})

process
    .on('uncaughtException', err => {
        errorLog = {
            ...new ErrorLog(),
            ...{
                message: err.message,
                type: err.name,
                stackTrace: err.stack,
                topic: 'UncaughtException',
                level: 'Error',
                title: 'An uncaught error occured'
            }
        };

        apiUtils.LogManagement.putErrorLogToQueue(errorLog);
    });

app.listen(8080, function () {
    console.log('Ready on port 8080');
});

module.exports = app;