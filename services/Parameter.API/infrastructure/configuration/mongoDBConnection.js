const mongoose = require('mongoose');
const config = require('./configurationManager');

function ConnectToMongoDB(connectionString) {
  var mongoDB =
    'mongodb://' +
    config.mongoDBUrl +
    ':' +
    config.mongoDBPort +
    '/parameter_db';
  mongoose
    .connect(mongoDB, {
      useNewUrlParser: true
    })
    .then(() => {
      console.log('Connected to database!');
    })
    .catch(() => {
      console.log('Connection failed!');
    });
}

module.exports = {
  connect: ConnectToMongoDB
};
