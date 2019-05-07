var cassandra = require("../infrastructure/configuration/cassandraConnection");
var apiUtils = require('../infrastructure/utils/apiUtils');
var PerformaceLog = require('../models/PerformanceLog');

exports.addPerformanceLog = (req, res, next) => {
    var performanceLog = {
        ...new PerformaceLog(),
        ...req.body
    };

    apiUtils.LogManagement.putPerformanceLogToQueue(performanceLog);

    res.status(200).json(performanceLog);
};

exports.updatePerformanceLog = (req, res, next) => {
    cassandra.instance.PerformanceLog.findOne({ id: req.body.id }, function (err, performanceLog) {
        const updatedLog = new cassandra.instance.PerformanceLog({
            ...req.body, ...{ elapsedMiliSecond: cassandra.datatypes.Long.fromString(req.body.elapsedMiliSecond.toString()) }
        });

        performanceLog = updatedLog;

        performanceLog.save(function (err) {
            if (err) {
                console.log(err);
                return;
            }
            console.log('updated!');
            res.status(200).json(updatedLog);
        });
    });
};

exports.deletePerformanceLog = (req, res, next) => {
    cassandra.instance.PerformanceLog.findOne({ id: req.params.id }, function (err, performanceLog) {
        performanceLog.delete(function (err) {
            if (err) {
                console.log(err);
                return;
            }
            console.log('deleted!');
            res.status(200).send();
        });
    });
};

exports.getPerformanceLogById = (req, res, next) => {
    console.log("by id");

    cassandra.instance.PerformanceLog.findOne({ id: req.params.id }, function (err, performanceLog) {
        res.status(200).json(performanceLog);
    });
};

exports.getPerformanceLogByTopic = (req, res, next) => {
    //TODO add paging
    //https://express-cassandra.readthedocs.io/en/stable/find/

    cassandra.instance.PerformanceLog.find({ topic: req.params.topic }, { materialized_view: 'PerformanceLogsByTopic', raw: true }, function (err, performanceLogs) {
        res.status(200).json(performanceLogs);
    });
};