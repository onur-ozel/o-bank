CREATE KEYSPACE IF NOT EXISTS log WITH replication = 
{'class':'SimpleStrategy','replication_factor':'1'};

CREATE TABLE log."ErrorLogs" ( 
    "id" text PRIMARY KEY,
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
    "help" text
);

CREATE TABLE log."PerformanceLogs" ( 
    "id" text PRIMARY KEY,
    "state" boolean,
    "sessionId" text,
    "lastModifiedDate" timestamp,
    "environment" text,
    "topic" text,
    "message" text,
    "stackTrace" text,
    "startTime" timestamp,
    "endTime" timestamp,
    "elapsedMiliSecond" bigint
);