var cassandra = require("../infrastructure/configuration/cassandraConnection");

exports.getErrorLogs = (req, res, next) => {
    cassandra.instance.ErrorLog.find({}, function (err, errorLogs) {
        if (err) {
            console.log(err);
            res.status(200).json(123);;
        }

        res.status(200).json(errorLogs);
    });
};

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
    cassandra.instance.ErrorLog.findOne({ id: req.params.id }, function (err, errorLog) {
        res.status(200).json(errorLog);
    });
};