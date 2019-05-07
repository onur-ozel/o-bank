CREATE KEYSPACE IF NOT EXISTS "Log" WITH replication = 
{'class':'SimpleStrategy','replication_factor':'1'};

CREATE TABLE "Log"."ErrorLogs" ( 
    "id" text,
    "state" boolean,
    "sessionId" text,
    "lastModifiedDate" bigint,
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

CREATE MATERIALIZED VIEW "Log"."ErrorLogsByEnvironment" AS
       SELECT "id","state","sessionId","sessionId","lastModifiedDate","environment","topic","type","code","level","title","message","stackTrace","help" 
       FROM "Log"."ErrorLogs"
       WHERE "environment" IS NOT NULL AND "lastModifiedDate" IS NOT NULL AND "id" IS NOT NULL
       PRIMARY KEY ("environment", "lastModifiedDate", "id")
       WITH CLUSTERING ORDER BY ("lastModifiedDate" DESC, "id" ASC);

CREATE TABLE "Log"."PerformanceLogs" ( 
    "id" text,
    "state" boolean,
    "sessionId" text,
    "lastModifiedDate" bigint,
    "environment" text,
    "topic" text,
    "message" text,
    "stackTrace" text,
    "startTime" bigint,
    "endTime" bigint,
    "elapsedMiliSecond" bigint,
    PRIMARY KEY ("id","lastModifiedDate")  
)
WITH CLUSTERING ORDER BY ("lastModifiedDate" DESC);

CREATE MATERIALIZED VIEW "Log"."PerformanceLogsByTopic" AS
       SELECT "id","state","sessionId","sessionId","lastModifiedDate","environment","topic","message","stackTrace","startTime","endTime","elapsedMiliSecond" 
       FROM "Log"."PerformanceLogs"
       WHERE "topic" IS NOT NULL AND "lastModifiedDate" IS NOT NULL AND "id" IS NOT NULL
       PRIMARY KEY ("topic", "lastModifiedDate", "id")
       WITH CLUSTERING ORDER BY ("lastModifiedDate" DESC, "id" ASC);       