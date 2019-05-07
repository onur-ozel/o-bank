module.exports = function ManagedError() {
    Error.captureStackTrace(this, this.constructor);
};

require('util').inherits(module.exports, Error);