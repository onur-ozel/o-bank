var kafkaConnection = require('../../configuration/kafkaConnection');
var errorSchema = require('./errorLogSchema');
var performanceSchema = require('./performanceLogSchema');

exports.putErrorLogToQueue = (errorLog) => {
    kafkaConnection.sendRecord('ErrorLog', errorLog, errorSchema, (err, data) => {
        if (err) {
            console.log(err);
        }

        console.log(data);
    });
}

exports.putPerformanceLogToQueue = (performanceLog) => {
    kafkaConnection.sendRecord('PerformanceLog', performanceLog, performanceSchema, (err, data) => {
        if (err) {
            console.log(err);
        }

        console.log(data);
    });
}