const uuidv1 = require('uuid/v1');


module.exports = function ManagedError() {
    Error.captureStackTrace(this, this.constructor);


    // this.id = uuidv1();
    // this.state = true;
    // this.lastModifiedDate = (new Date());
    // // this.environment = environment;
    // // this.topic = topic;
    // // this.type = type;
    // // this.code = code;
    // // this.level = level;
    // // this.title = title;
    // // this.message = message;
    // // // this.stackTrace = stackTrace;
    // // this.help = help;
};

// id: "text",
// state: "boolean",
// sessionId: "text",
// lastModifiedDate: "timestamp",
// environment: "text",
// topic: "text",
// type: "text",
// code: "text",
// level: "text",
// title: "text",
// message: "text",
// stackTrace: "text",
// help: "text"

require('util').inherits(module.exports, Error);