var models = require("../infrastructure/configuration/cassandraConnection");


exports.getErrorLogs = (req, res, next) => {
    models.instance.ErrorLog.find({}, function (err, errorLogs) {
        if (err) {
            console.log(err);
            res.status(200).json(123);;
        }
        res.status(200).json(errorLogs);
    });
};