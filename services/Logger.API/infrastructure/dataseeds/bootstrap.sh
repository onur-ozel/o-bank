#!/bin/bash
set -e

# First arg is `-f` or `--some-option` then prepend default cassandra command to option(s)
if [ "${1:0:1}" = '-' ]; then
  set -- cassandra -f "$@"
fi

# If we're starting DSE
if [ "$1" = 'cassandra' -a "$2" = '-f' ]; then
  # See if we've already completed bootstrapping
  if [ ! -f logger_data_cassandra_bootstrapped ]; then
    echo 'Setting up Logger Cassandra database'

    # Invoke the entrypoint script to start Cassandra as a background job and get the pid
    # starting Cassandra in the background the first time allows us to monitor progress and register schema
    echo '=> Starting Cassandra'
    /docker-entrypoint.sh "$@" &
    cassandra_pid="$!"

    # Wait for port 9042 (CQL) to be ready for up to 240 seconds
    echo '=> Waiting for Cassandra to become available'
    /wait-for-it.sh -t 240 127.0.0.1:9042
    echo '=> Cassandra is available'

    # Create the keyspace if necessary
    echo '=> Ensuring log keyspace is created'
    cqlsh -f /logdb-init.cql 127.0.0.1 9042

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
    touch logger_data_cassandra_bootstrapped

    # Now allow Cassandra to start normally below
    echo 'Logger Cassandra database has been setup, starting Cassandra normally'
  fi
fi

# Run the main entrypoint script from the base image
exec /docker-entrypoint.sh "$@"