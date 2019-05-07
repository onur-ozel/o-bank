module.exports = {
    fields: {
        id: "text",
        state: "boolean",
        sessionId: "text",
        lastModifiedDate: "bigint",
        environment: "text",
        topic: "text",
        type: "text",
        code: "text",
        level: "text",
        title: "text",
        message: "text",
        stackTrace: "text",
        help: "text"

    },
    key: [["id"], "lastModifiedDate"],
    clustering_order: { "lastModifiedDate": "desc" },
    materialized_views: {
        "ErrorLogsByEnvironment": {
            select: ["environment", "lastModifiedDate", "id", "code", "help", "level", "message", "sessionId", "stackTrace", "state", "title", "topic", "type"],
            key: [["environment"], "lastModifiedDate", "id"],
            clustering_order: { "lastModifiedDate": "DESC", "id": "ASC" },
            filters: {
                "environment": { $isnt: null },
                "lastModifiedDate": { $isnt: null },
                "id": { $isnt: null }
            }
        }
    },
    table_name: "ErrorLogs"
};