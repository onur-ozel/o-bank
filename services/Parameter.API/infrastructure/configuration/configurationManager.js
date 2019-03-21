const currentEnvironment = process.env.RUNNING_ENVIRONMENT || 'Development';

var appConfig = require('../configFiles/appSettings.' +
  currentEnvironment +
  '.json');

module.exports = appConfig;
