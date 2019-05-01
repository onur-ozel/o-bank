var uuidv1 = require('uuid/v1');

function ErrorLog() {       // Accept name and age in the constructor
    this.id = uuidv1();
    this.state = true;
    this.sessionId = 'sessionId';
    this.lastModifiedDate = (new Date());
    this.environment = 'Logger.API';
    this.topic = null;
    this.type = null;
    this.code = null;
    this.level = null;
    this.title = null;
    this.message = null;
    this.stackTrace = null;
    this.help = null;
}

module.exports = ErrorLog;     // Export the Cat function as it is