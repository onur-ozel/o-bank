#!/bin/bash
set -e

# First arg is `-f` or `--some-option` then prepend default cassandra command to option(s)
if [ "${1:0:1}" = '-' ]; then
  set -- cassandra -f "$@"
fi

echo $1
echo $2

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
    cqlsh -f /log-init.cql 127.0.0.1 9042

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

    # Now allow DSE to start normally below
    echo 'Logger Cassandra database has been setup, starting DSE normally'
  fi
fi

# Run the main entrypoint script from the base image
exec /docker-entrypoint.sh "$@"












--------------------------








log-init.cql

CREATE KEYSPACE IF NOT EXISTS log WITH replication = 
{'class':'SimpleStrategy','replication_factor':'1'};

CREATE TABLE log.error_log ( ip text PRIMARY KEY, message text);

INSERT INTO log.error_log (ip,message) VALUES ('123','messageasd');



-----------


Docker.Logger.Data.Cassandra.WithData.Dockerfile

FROM cassandra:latest


COPY [ "./infrastructure/dataseeds/", "/" ]

ENTRYPOINT ["/bootstrap.sh"]

CMD ["cassandra", "-f"]

--------------------
docker-compose.Logger.API.yml


version: '3.4'
services:
  logger.data.cassandra:
    build:
      context: .
      dockerfile: Docker.Logger.Data.Cassandra.WithData.Dockerfile
    ports:
      - '9042:9042'

--------------------


wait-for-it.sh


#!/usr/bin/env bash
#   Use this script to test if a given TCP host/port are available

cmdname=$(basename $0)

echoerr() { if [[ $QUIET -ne 1 ]]; then echo "$@" 1>&2; fi }

usage()
{
    cat << USAGE >&2
Usage:
    $cmdname host:port [-s] [-t timeout] [-- command args]
    -h HOST | --host=HOST       Host or IP under test
    -p PORT | --port=PORT       TCP port under test
                                Alternatively, you specify the host and port as host:port
    -s | --strict               Only execute subcommand if the test succeeds
    -q | --quiet                Don't output any status messages
    -t TIMEOUT | --timeout=TIMEOUT
                                Timeout in seconds, zero for no timeout
    -- COMMAND ARGS             Execute command with args after the test finishes
USAGE
    exit 1
}

wait_for()
{
    if [[ $TIMEOUT -gt 0 ]]; then
        echoerr "$cmdname: waiting $TIMEOUT seconds for $HOST:$PORT"
    else
        echoerr "$cmdname: waiting for $HOST:$PORT without a timeout"
    fi
    start_ts=$(date +%s)
    while :
    do
        if [[ $ISBUSY -eq 1 ]]; then
            nc -z $HOST $PORT
            result=$?
        else
            (echo > /dev/tcp/$HOST/$PORT) >/dev/null 2>&1
            result=$?
        fi
        if [[ $result -eq 0 ]]; then
            end_ts=$(date +%s)
            echoerr "$cmdname: $HOST:$PORT is available after $((end_ts - start_ts)) seconds"
            break
        fi
        sleep 1
    done
    return $result
}

wait_for_wrapper()
{
    # In order to support SIGINT during timeout: http://unix.stackexchange.com/a/57692
    if [[ $QUIET -eq 1 ]]; then
        timeout $BUSYTIMEFLAG $TIMEOUT $0 --quiet --child --host=$HOST --port=$PORT --timeout=$TIMEOUT &
    else
        timeout $BUSYTIMEFLAG $TIMEOUT $0 --child --host=$HOST --port=$PORT --timeout=$TIMEOUT &
    fi
    PID=$!
    trap "kill -INT -$PID" INT
    wait $PID
    RESULT=$?
    if [[ $RESULT -ne 0 ]]; then
        echoerr "$cmdname: timeout occurred after waiting $TIMEOUT seconds for $HOST:$PORT"
    fi
    return $RESULT
}

# process arguments
while [[ $# -gt 0 ]]
do
    case "$1" in
        *:* )
        hostport=(${1//:/ })
        HOST=${hostport[0]}
        PORT=${hostport[1]}
        shift 1
        ;;
        --child)
        CHILD=1
        shift 1
        ;;
        -q | --quiet)
        QUIET=1
        shift 1
        ;;
        -s | --strict)
        STRICT=1
        shift 1
        ;;
        -h)
        HOST="$2"
        if [[ $HOST == "" ]]; then break; fi
        shift 2
        ;;
        --host=*)
        HOST="${1#*=}"
        shift 1
        ;;
        -p)
        PORT="$2"
        if [[ $PORT == "" ]]; then break; fi
        shift 2
        ;;
        --port=*)
        PORT="${1#*=}"
        shift 1
        ;;
        -t)
        TIMEOUT="$2"
        if [[ $TIMEOUT == "" ]]; then break; fi
        shift 2
        ;;
        --timeout=*)
        TIMEOUT="${1#*=}"
        shift 1
        ;;
        --)
        shift
        CLI=("$@")
        break
        ;;
        --help)
        usage
        ;;
        *)
        echoerr "Unknown argument: $1"
        usage
        ;;
    esac
done

if [[ "$HOST" == "" || "$PORT" == "" ]]; then
    echoerr "Error: you need to provide a host and port to test."
    usage
fi

TIMEOUT=${TIMEOUT:-15}
STRICT=${STRICT:-0}
CHILD=${CHILD:-0}
QUIET=${QUIET:-0}

# check to see if timeout is from busybox?
# check to see if timeout is from busybox?
TIMEOUT_PATH=$(realpath $(which timeout))
if [[ $TIMEOUT_PATH =~ "busybox" ]]; then
        ISBUSY=1
        BUSYTIMEFLAG="-t"
else
        ISBUSY=0
        BUSYTIMEFLAG=""
fi

if [[ $CHILD -gt 0 ]]; then
    wait_for
    RESULT=$?
    exit $RESULT
else
    if [[ $TIMEOUT -gt 0 ]]; then
        wait_for_wrapper
        RESULT=$?
    else
        wait_for
        RESULT=$?
    fi
fi

if [[ $CLI != "" ]]; then
    if [[ $RESULT -ne 0 && $STRICT -eq 1 ]]; then
        echoerr "$cmdname: strict mode, refusing to execute subprocess"
        exit $RESULT
    fi
    exec "${CLI[@]}"
else
    exit $RESULT
fi
