var cassandra = require("../infrastructure/configuration/cassandraConnection");
var apiUtils = require("../infrastructure/utils/apiUtils");

exports.addErrorLog = (req, res, next) => {
    console.log('add');
    const errorLog = new cassandra.instance.ErrorLog({
        ...req.body
    });

    errorLog.save(function (err) {
        if (err) {
            console.log(err);
            return;
        }
        console.log('inserted!');

        res.status(200).json(errorLog);
    });
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
    console.log("by environment");


    cassandra.instance.ErrorLog.eachRow({ environment: req.params.environment }, { materialized_view: 'ErrorLogsByEnvironment', raw: true, fetchSize: 5 }, function (n, row) {
        // invoked per each row in all the pages
    }, function (err, errorLogs) {
        // called once the page has been retrieved.
        if (err) throw err;
        if (errorLogs.nextPage) {
            res.status(200).json(errorLogs);
            errorLogs.nextPage();
        }
    });

    // cassandra.instance.ErrorLog.find({ environment: req.params.environment }, { materialized_view: 'ErrorLogsByEnvironment', raw: true }, function (err, errorLogs) {
    //     res.status(200).json(errorLogs);
    // });
};