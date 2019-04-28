var cassandra = require("../infrastructure/configuration/cassandraConnection");
var apiUtils = require("../infrastructure/utils/apiUtils");

exports.getErrorLogs = (req, res, next) => {
    // var query = {
    //     // equal query stays for name='john', also could be written as name: { $eq: 'John' }
    //     environment: 'Customer.API',
    //     id: '4',
    //     // range query stays for age>10 and age<=20. You can use $gt (>), $gte (>=), $lt (<), $lte (<=)
    //     // age : { '$gt':10, '$lte':20 },
    //     // // IN clause, means surname should either be Doe or Smith
    //     // surname : { '$in': ['Doe','Smith'] },
    //     // // like query supported by sasi indexes, complete_name must have an SASI index defined in custom_indexes
    //     // complete_name: { '$like': 'J%' },
    //     // // order results by age in ascending order.
    //     // // also allowed $desc and complex order like $orderby: {'$asc' : ['k1','k2'] }
    //     // $orderby: { '$asc' :'age' },
    //     // // group results by a certain field or list of fields
    //     // $groupby: [ 'age' ],
    //     //limit the result set to 10 rows
    //     $select: { select: ['id'], distinct: true },
    //     $limit: 10
    // }

    var query = {};
    var queryOptions = { raw: true };

    query = apiUtils.dynamicWhere(req.query.searches, query);

    queryOptions = apiUtils.dynamicSelect(req.query.fields, queryOptions);


    // apiUtils.dynamicTake(req.query.fields, queryOptions);
    cassandra.instance.ErrorLog.find(query, queryOptions, function (err, errorLogs) {
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