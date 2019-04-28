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
    key: ["environment", "topic", "sessionId", "id", "lastModifiedDate"],
    clustering_order: { "topic": "asc", "sessionId": "asc", "id": "asc", "lastModifiedDate": "desc" },
    table_name: "PerformanceLogs"
};