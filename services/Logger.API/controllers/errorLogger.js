var cassandra = require('../infrastructure/configuration/cassandraConnection');
var apiUtils = require('../infrastructure/utils/apiUtils');
var ErrorLog = require('../models/ErrorLog');

exports.addErrorLog = (req, res, next) => {
    if (!req.body.id) {
        var error = new apiUtils.ManagedError();
        error.topic = 'ErrorLogCRUD';
        error.type = 'ArgumentNullException';
        error.level = 'Error';
        error.title = 'Undefined required fields!';
        error.message = 'Id field is required';

        throw error;
    }

    var errorLog = {
        ...new ErrorLog(),
        ...req.body
    };

    apiUtils.LogManagement.putErrorLogToQueue(errorLog);

    res.status(200).json(errorLog);
};

exports.updateErrorLog = (req, res, next) => {
    cassandra.instance.ErrorLog.findOne({ id: req.body.id }, function (err, errorLog) {
        const updatedLog = new cassandra.instance.ErrorLog({
            ...req.body
        });

        errorLog = updatedLog;

        errorLog.save(function (err) {
            if (err) {
                console.log(err);
                return;
            }
            console.log('updated!');
            res.status(200).json(updatedLog);
        });
    });
};

exports.deleteErrorLog = (req, res, next) => {
    cassandra.instance.ErrorLog.findOne({ id: req.params.id }, function (err, errorLog) {
        errorLog.delete(function (err) {
            if (err) {
                console.log(err);
                return;
            }
            console.log('deleted!');
            res.status(200).send();
        });
    });
};

exports.getErrorLogById = (req, res, next) => {
    console.log("by id");

    cassandra.instance.ErrorLog.findOne({ id: req.params.id }, function (err, errorLog) {
        res.status(200).json(errorLog);
    });
};

exports.getErrorLogByEnvironment = (req, res, next) => {
    //TODO add paging
    //https://express-cassandra.readthedocs.io/en/stable/find/

    cassandra.instance.ErrorLog.find({ environment: req.params.environment }, { materialized_view: 'ErrorLogsByEnvironment', raw: true }, function (err, errorLogs) {
        res.status(200).json(errorLogs);
    });
};