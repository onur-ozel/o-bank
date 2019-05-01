var uuidv1 = require('uuid/v1');

function ErrorLog() {       // Accept name and age in the constructor
    this.id = uuidv1();
    this.state = true;
    this.session_id = 'sessionId';
    this.last_modified_date = (new Date());
    this.environment = 'Logger.API';
    this.topic = null;
    this.type = null;
    this.code = null;
    this.level = null;
    this.title = null;
    this.message = null;
    this.stack_trace = null;
    this.help = null;
}

module.exports = ErrorLog;     // Export the Cat function as it is