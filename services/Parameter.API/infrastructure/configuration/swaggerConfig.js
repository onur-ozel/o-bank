const swaggerJsdoc = require('swagger-jsdoc');

let swaggerOptions = {
  swaggerDefinition: {
    swagger: '2.0',
    info: {
      title: 'API Explorer', // Title (required)
      version: '1.0.0', // Version (required)
      contact: {
        name: '',
        url: ''
      }
    }
  },
  apis: ['./routes/*.js']
};

const specs = swaggerJsdoc(swaggerOptions);

module.exports = specs;
