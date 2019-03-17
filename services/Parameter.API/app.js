var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

var countryRouter = require('./routes/country');

var app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/country', countryRouter);

// swagger

const swaggerJsdoc = require('swagger-jsdoc');

let swaggerOptions = {
  swaggerDefinition: {
    swagger: '2.0',
    info: {
      title: 'API Explorer', // Title (required)
      version: '1.0.0', // Version (required)
      contact: { name: '', url: '' }
    }
  },
  apis: [
    './routes/*.js'
  ]
};

const specs = swaggerJsdoc(swaggerOptions);

console.log('./routes/*.js');

const swaggerUi = require('swagger-ui-express');
// const swaggerDocument = require('./swagger.json');

// app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(specs));

// swagger

app.listen(8080, function() {
  console.log('Ready on port 8080');
});

module.exports = app;
