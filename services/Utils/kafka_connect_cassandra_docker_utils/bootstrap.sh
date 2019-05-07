#!/bin/bash
set -e

# If we're starting DSE
if [ "$1" = '/etc/confluent/docker/run' ]; then
  # See if we've already completed bootstrapping
  if [ ! -f kafka_connect_cassandra_bootstrapped ]; then
    echo 'Setting up Logger Cassandra database'

    # Invoke the entrypoint script to start Cassandra as a background job and get the pid
    # starting Cassandra in the background the first time allows us to monitor progress and register schema
    echo '=> Starting Cassandra'
    /etc/confluent/docker/run &
    cassandra_pid="$!"

    # Wait for port 9042 (CQL) to be ready for up to 240 seconds
    echo '=> Waiting for Cassandra to become available'
    /wait-for-it.sh -t 240 127.0.0.1:8083
    echo '=> Cassandra is available'

    curl -X POST \
         -H "Content-Type: application/json" \
         --data '{
         "name" : "ErrorLoggerCassandraSink",
         "config": {
          "connector.class": "com.obank.kafka.connect.cassandra.OBankCassandraSinkConnector",
          "cassandra.host": "logger.data",
          "topics": "ErrorLog",
          "cassandra.table": "ErrorLogs",
          "name": "ErrorLoggerCassandraSink",
          "cassandra.port": "9042",
          "cassandra.keyspace": "Log",
          "value.converter": "io.confluent.connect.avro.AvroConverter",
          "key.converter": "org.apache.kafka.connect.storage.StringConverter",
          "key.converter.schema.registry.url":"http://schema-registry:8081",
          "value.converter.schema.registry.url":"http://schema-registry:8081"
        }}
    ' \
    http://localhost:8083/connectors

    curl -X POST \
         -H "Content-Type: application/json" \
         --data '{
         "name" : "PerformanceLoggerCassandraSink",
         "config": {
          "connector.class": "com.obank.kafka.connect.cassandra.OBankCassandraSinkConnector",
          "cassandra.host": "logger.data",
          "topics": "PerformanceLog",
          "cassandra.table": "PerformanceLogs",
          "name": "PerformanceLoggerCassandraSink",
          "cassandra.port": "9042",
          "cassandra.keyspace": "Log",
          "value.converter": "io.confluent.connect.avro.AvroConverter",
          "key.converter": "org.apache.kafka.connect.storage.StringConverter",
          "key.converter.schema.registry.url":"http://schema-registry:8081",
          "value.converter.schema.registry.url":"http://schema-registry:8081"
        }}
    ' \
    http://localhost:8083/connectors    

    # Shutdown Cassandra after bootstrapping to allow the entrypoint script to start normally
    echo '=> Shutting down Casssandra after bootstrapping'
    kill -s TERM "$cassandra_pid"

    # Cassandra will exit with code 143 (128 + 15 SIGTERM) once stopped
    set +e
    wait "$cassandra_pid"
    if [ $? -ne 143 ]; then
      echo >&2 'Logger Cassandra database setup failed'
      exit 1
    fi
    set -e

    # Don't bootstrap next time we start
    touch kafka_connect_cassandra_bootstrapped

    # Now allow Cassandra to start normally below
    echo 'Logger Cassandra database has been setup, starting Cassandra normally'
  fi
fi

# Run the main entrypoint script from the base image
exec /etc/confluent/docker/run