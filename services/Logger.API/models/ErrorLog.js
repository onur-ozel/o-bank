module.exports = {
    fields: {
        id: "text",
        state: "boolean",
        sessionId: "text",
        lastModifiedDate: "timestamp",
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
    key: ["id"],
    table_name: "ErrorLogs"
};