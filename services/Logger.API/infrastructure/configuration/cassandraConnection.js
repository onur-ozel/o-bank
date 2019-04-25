var ExpressCassandra = require('express-cassandra');

var ErrorLog = require("../../models/ErrorLog");
var PerformanceLog = require("../../models/PerformanceLog");

var cassandra = ExpressCassandra.createClient({
    clientOptions: {
        contactPoints: ['localhost'],
        protocolOptions: { port: 9042 },
        keyspace: 'log',
        queryOptions: { consistency: ExpressCassandra.consistencies.one }
    },
    ormOptions: {
        defaultReplicationStrategy: {
            class: 'SimpleStrategy',
            replication_factor: 1
        },
        disableTTYConfirmation: true,
        migration: 'safe'
    }
});

cassandra.loadSchema('ErrorLog', ErrorLog).syncDB(function (err, result) {
    if (err) throw err;
});

cassandra.loadSchema('PerformanceLog', PerformanceLog).syncDB(function (err, result) {
    if (err) throw err;
});

module.exports = cassandra;