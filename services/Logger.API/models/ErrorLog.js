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
    key : ["environment","topic","sessionId","id","lastModifiedDate"],
    clustering_order: {"topic":"asc","sessionId":"asc","id":"asc","lastModifiedDate": "desc"},
    table_name: "ErrorLogs"
};