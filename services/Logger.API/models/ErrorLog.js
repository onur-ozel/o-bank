var uuidv1 = require('uuid/v1');

function ErrorLog() {
    this.id = uuidv1();
    this.state = true;
    this.sessionId = 'sessionId';
    this.lastModifiedDate = (new Date()).getTime();
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

module.exports = ErrorLog;  