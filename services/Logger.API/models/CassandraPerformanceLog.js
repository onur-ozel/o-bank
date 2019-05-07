module.exports = {
    fields: {
        id: "text",
        state: "boolean",
        sessionId: "text",
        lastModifiedDate: "bigint",
        environment: "text",
        topic: "text",
        message: "text",
        stackTrace: "text",
        startTime: "bigint",
        endTime: "bigint",
        elapsedMiliSecond: "bigint"
    },
    key: [["id"], "lastModifiedDate"],
    clustering_order: { "lastModifiedDate": "DESC" },
    materialized_views: {
        "PerformanceLogsByTopic": {
            select: ["topic", "lastModifiedDate", "id", "elapsedMiliSecond", "endTime", "environment", "message", "sessionId", "stackTrace", "startTime", "state"],
            key: [["topic"], "lastModifiedDate", "id"],
            clustering_order: { "lastModifiedDate": "DESC", "id": "ASC" },
            filters: {
                "topic": { $isnt: null },
                "lastModifiedDate": { $isnt: null },
                "id": { $isnt: null }
            }
        }
    },
    table_name: "PerformanceLogs"
};