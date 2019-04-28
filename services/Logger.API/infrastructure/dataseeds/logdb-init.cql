CREATE KEYSPACE IF NOT EXISTS log WITH replication = 
{'class':'SimpleStrategy','replication_factor':'1'};

CREATE TABLE log."ErrorLogs" ( 
    "id" text,
    "state" boolean,
    "sessionId" text,
    "lastModifiedDate" timestamp,
    "environment" text,
    "topic" text,
    "type" text,
    "code" text,
    "level" text,
    "title" text,
    "message" text,
    "stackTrace" text,
    "help" text,
    PRIMARY KEY ("environment","topic","sessionId","id","lastModifiedDate") 
) WITH CLUSTERING ORDER BY ("topic" ASC,"sessionId" ASC,"id" ASC,"lastModifiedDate" DESC);

CREATE TABLE log."PerformanceLogs" ( 
    "id" text,
    "state" boolean,
    "sessionId" text,
    "lastModifiedDate" timestamp,
    "environment" text,
    "topic" text,
    "message" text,
    "stackTrace" text,
    "startTime" timestamp,
    "endTime" timestamp,
    "elapsedMiliSecond" bigint,
    PRIMARY KEY ("environment","topic","sessionId","id","lastModifiedDate") 
) WITH CLUSTERING ORDER BY ("topic" ASC,"sessionId" ASC,"id" ASC,"lastModifiedDate" DESC);

https://www.datastax.com/dev/blog/new-in-cassandra-3-0-materialized-views