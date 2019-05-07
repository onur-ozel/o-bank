var ExpressCassandra = require('express-cassandra');

var CasssandraErrorLog = require("../../models/CasssandraErrorLog");
var CassandraPerformanceLog = require("../../models/CassandraPerformanceLog");

const config = require('./configurationManager');

var cassandra = ExpressCassandra.createClient({
    clientOptions: {
        contactPoints: [config.cassandraUrl],
        protocolOptions: { port: config.cassandraPort },
        keyspace: 'Log',
        queryOptions: { consistency: ExpressCassandra.consistencies.one }
    },
    ormOptions: {
        defaultReplicationStrategy: {
            class: 'SimpleStrategy',
            replication_factor: 1
        },
        disableTTYConfirmation: true,
        migration: 'alter'
    }
});

cassandra.loadSchema('ErrorLog', CasssandraErrorLog).syncDB(function (err, result) {
    if (err) throw err;
});

cassandra.loadSchema('PerformanceLog', CassandraPerformanceLog).syncDB(function (err, result) {
    if (err) throw err;
});

module.exports = cassandra;