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
    PRIMARY KEY ("id", "lastModifiedDate") 
)
WITH CLUSTERING ORDER BY ("lastModifiedDate" DESC);

CREATE MATERIALIZED VIEW log."ErrorLogsByEnvironment" AS
       SELECT "id","state","sessionId","sessionId","lastModifiedDate","environment","topic","type","code","level","title","message","stackTrace","help" 
       FROM log."ErrorLogs"
       WHERE "environment" IS NOT NULL AND "lastModifiedDate" IS NOT NULL AND "id" IS NOT NULL
       PRIMARY KEY ("environment", "lastModifiedDate", "id")
       WITH CLUSTERING ORDER BY ("lastModifiedDate" DESC, "id" ASC);

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
    PRIMARY KEY ("id","lastModifiedDate")  
)
WITH CLUSTERING ORDER BY ("lastModifiedDate" DESC);

CREATE MATERIALIZED VIEW log."PerformanceLogsByTopic" AS
       SELECT "id","state","sessionId","sessionId","lastModifiedDate","environment","topic","message","stackTrace","startTime","endTime","elapsedMiliSecond" 
       FROM log."PerformanceLogs"
       WHERE "topic" IS NOT NULL AND "lastModifiedDate" IS NOT NULL AND "id" IS NOT NULL
       PRIMARY KEY ("topic", "lastModifiedDate", "id")
       WITH CLUSTERING ORDER BY ("lastModifiedDate" DESC, "id" ASC);       