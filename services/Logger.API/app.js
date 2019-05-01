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

//kafka

var kafka = require('kafka-node'),
    Producer = kafka.Producer,
    client = new kafka.KafkaClient({ kafkaHost: 'localhost:9092' }),
    producer = new Producer(client);


// producer.on('ready', function () {
//     producer.send(kafkaMessage, function (err, data) {
//         console.log(data);
//     });
// });

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

    handleError(errorLog);
})

process
    .on('unhandledRejection', (reason, p) => {
        console.error(reason, 'Unhandled Rejection at Promise', p);
    })
    .on('uncaughtException', err => {
        console.error(err, 'Uncaught Exception thrown');
        process.exit(1);
    });

function handleError(errorLog) {

    // KeyedMessage = kafka.KeyedMessage;
    // km = new KeyedMessage('id', JSON.stringify({ id: '3asd' }));
    var kafkaMessage = {
        topic: 'log5',
        messages: JSON.stringify(errorLog), // multi messages should be a array, single message can be just a string or a KeyedMessage instance
        // key: 'id' // string or buffer, only needed when using keyed partitioner
    };

    var kafkaMessages = [kafkaMessage];

    producer.send(kafkaMessages, function (err, data) {
        console.log(data);
    });
}

app.listen(8080, function () {
    console.log('Ready on port 8080');
});

module.exports = app;