module.exports = {
    fields: {
        id: "text",
        state: "boolean",
        sessionId: "text",
        lastModifiedDate: "timestamp",
        environment: "text",
        topic: "text",
        message: "text",
        stackTrace: "text",
        startTime: "timestamp",
        endTime: "timestamp",
        elapsedMiliSecond: "bigint"
    },
    key: ["id"],
    table_name: "PerformanceLogs"
};