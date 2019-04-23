module.exports = {
    fields: {
        id: "text",
        state: "boolean",
        sessionId: "text",
        lastModifiedDate: "timestamp",
        logType: "text",
        environment: "text",
        type: "text",
        message: "text",
        stackTrace: "text",
        startTime: "timestamp",
        endTime: "timestamp",
        elapsedMiliSecond: "int"
    },
    key: ["id"]
};