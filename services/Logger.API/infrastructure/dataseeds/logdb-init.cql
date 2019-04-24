CREATE KEYSPACE IF NOT EXISTS log WITH replication = 
{'class':'SimpleStrategy','replication_factor':'1'};

CREATE TABLE log.ErrorLogs ( 
    id text PRIMARY KEY,
    state boolean,
    sessionId text,
    lastModifiedDate timestamp,
    environmentName text,
    topic text,
    type text,
    code text,
    level text,
    title text,
    message text,
    stackTrace text,
    help text
);

CREATE TABLE log.PerformanceLogs ( 
    id text PRIMARY KEY,
    state boolean,
    sessionId text,
    lastModifiedDate timestamp,
    environmentName text,
    topic text,
    message text,
    stackTrace text,
    startTime timestamp,
    endTime timestamp,
    elapsedMiliSecond bigint
);



INSERT INTO log.performanceLog (id,state,sessionId,environmentName,elapsedMiliSecond) VALUES ('123',true,'asdasd','asdasd23',12345);