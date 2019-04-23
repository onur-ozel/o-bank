var ExpressCassandra = require('express-cassandra');

var ErrorLog = require("../../models/ErrorLog");
var PerformanceLog = require("../../models/PerformanceLog");

var models = ExpressCassandra.createClient({
    clientOptions: {
        contactPoints: ['127.0.0.1'],
        protocolOptions: { port: 9042 },
        keyspace: 'mykeyspace',
        queryOptions: { consistency: ExpressCassandra.consistencies.one }
    },
    ormOptions: {
        defaultReplicationStrategy: {
            class: 'SimpleStrategy',
            replication_factor: 1
        },
        disableTTYConfirmation: true,
        migration: 'alter',
    }
});

models.loadSchema('ErrorLog', ErrorLog).syncDB(function (err, result) {
    if (err) throw err;
});

models.loadSchema('PerformanceLog', PerformanceLog).syncDB(function (err, result) {
    if (err) throw err;
});

module.exports = models;