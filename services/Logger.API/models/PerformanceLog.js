var uuidv1 = require('uuid/v1');

function PerformanceLog() {
    this.id = uuidv1();
    this.state = true;
    this.sessionId = 'sessionId';
    this.lastModifiedDate = (new Date()).getTime();
    this.environment = 'Logger.API';
    this.topic = null;
    this.message = null;
    this.stackTrace = null;
    this.startTime = null;
    this.endTime = null;
    this.elapsedMiliSecond = null;
}

module.exports = PerformanceLog;   