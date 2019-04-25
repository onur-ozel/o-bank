var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');
// var uuidv1 = require('uuid/v1');

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

// var models = require("./infrastructure/configuration/cassandraConnection");

// var performance = new models.instance.PerformanceLog({
//     id: uuidv1(),
//     state: true,
//     sessionId: "MVG9lKcPoNINVBIPJjdw1J9LLJbP_pqwoJYyuis",
//     lastModifiedDate: new Date(),
//     logType: "Performance",
//     environment: "Server",
//     type: "APIResponse",
//     message: "performance message",
//     stackTrace: "trace",
//     startTime: new Date((new Date()).getTime() - 1233),
//     endTime: new Date((new Date()).getTime() + 1233),
//     elapsedMiliSecond: 123123
// });
// performance.save(function (err) {
//     if (err) {
//         console.log(err);
//         return;
//     }
//     console.log('saved performance!');
// });


// var error = new models.instance.ErrorLog({
//     id: uuidv1(),
//     state: true,
//     sessionId: "MVG9lKcPoNINVBIPJjdw1J9LLJbP_pqwoJYyuis",
//     lastModifiedDate: new Date(),
//     logType: "Error",
//     environment: "Server",
//     type: "NullPointerException",
//     code: 123323,
//     level: "Fatal",
//     title: "title",
//     message: "error message",
//     stackTrace: "trace",
//     help: "hrlp"
// });
// error.save(function (err) {
//     if (err) {
//         console.log(err);
//         return;
//     }
//     console.log('saved error!');
// });

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

app.listen(8080, function () {
    console.log('Ready on port 8080');
});

module.exports = app;