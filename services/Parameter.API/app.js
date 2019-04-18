var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

//connect to mongo
mongoConnection = require('./infrastructure/configuration/mongoDBConnection');
mongoConnection.connect();

var app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(
  express.urlencoded({
    extended: false
  })
);
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

//swagger utilization
const swaggerUi = require('swagger-ui-express');
const yaml = require('yamljs');
const swaggerDoc = yaml.load('./resources/swagger/Parameter.API.v1.yaml');
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerDoc));

//routers
var countryRouter = require('./routes/country');
app.use('/country', countryRouter);

app.listen(8080, function() {
  console.log('Ready on port 8080');
});

module.exports = app;
