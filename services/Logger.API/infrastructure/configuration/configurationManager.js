const currentEnvironment = process.env.RUNNING_ENVIRONMENT || 'Development';

var appConfig = require('../../resources/configFiles/appSettings.' +
  currentEnvironment +
  '.json');

module.exports = appConfig;