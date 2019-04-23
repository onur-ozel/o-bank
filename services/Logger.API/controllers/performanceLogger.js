var models = require("../infrastructure/configuration/cassandraConnection");

exports.getPerformanceLogs = (req, res, next) => {
    models.instance.PerformanceLog.find({}, function (err, performanceLogs) {
        if (err) {
            console.log(err);
            res.status(200).json(123);;
        }
        res.status(200).json(performanceLogs);
    });
};