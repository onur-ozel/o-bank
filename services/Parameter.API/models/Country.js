--models/country.js
const mongoose = require("mongoose");

const countrySchema = mongoose.Schema({
  flag: {
    type: String
  },
  name: {
    type: String,
    required: true
  },
  alpha2Code: {
    type: String
  },
  alpha3Code: {
    type: String,
    required: true
  },
  capital: {
    type: String
  },
  region: {
    type: String
  },
  subregion: {
    type: String
  },
  demonym: {
    type: String
  },
  nativeName: {
    type: String
  },
  numericCode: {
    type: String
  }
}, {
  collection: 'Country'
});

module.exports = mongoose.model("Country", countrySchema);


--controls/country.js

const Country = require("../models/country");
const cacheManager = require('../infrastructure/configuration/cacheManager');

var cacheName = 'Parameters_Countries';

exports.getCountries = (req, res, next) => {
    var returnObject;

    if (req.query.pageIndex && req.query.pageSize) {
        getDataWithPaging(req.query.pageIndex, req.query.pageSize, res);
    } else {
        cacheManager.getFromCache(cacheName, (error, result) => {
            returnObject = result;

            if (returnObject == null) {
                Country.find().then(result => {
                    returnObject = result;

                    cacheManager.setToJSONCache(cacheName, returnObject);

                    res.status(200).json(returnObject);
                });
            } else {
                res.status(200).json(JSON.parse(returnObject));
            }
        });
    }
};

function getDataWithPaging(pageIndex, pageSize, res) {
    var pageIndex = parseInt(pageIndex);
    var pageSize = parseInt(pageSize);

    var promises = [
        Country.find().skip(pageSize * pageIndex).limit(pageSize).exec(),
        Country.estimatedDocumentCount().exec()
    ];

    Promise.all(promises).then(function (results) {
        res.status(200).json({
            PageIndex: pageIndex,
            PageSize: pageSize,
            Count: results[1],
            Data: results[0]
        });
    });
};

exports.createCountry = (req, res, next) => {
    const country = new Country({
        ...req.body
    });

    country.save().then(createdCountry => {
        res.status(201).json({
            message: "Post added successfully",
            post: createdCountry
        });
    });

    cacheManager.clearCache(cacheName);
};


exports.updateCountry = function (req, res, next) {
    Country.findOneAndUpdate({
        _id: req.params.id
    }, req.body).then((result) => {
        res.status(200).json({
            result: result
        });
    });

    cacheManager.clearCache(cacheName);
};

exports.deleteCountry = function (req, res, next) {
    Country.findOneAndDelete({
        _id: req.params.id
    }).then((result) => {
        res.status(200).json({
            result: result
        });
    });

    cacheManager.clearCache(cacheName);
};

--infrastructure/configuration/cacheManager.js

const redis = require('redis');
const config = require('./configurationManager');

class CacheManager {

    constructor() {
        this.cachePort = config.redisPort;
        this.cacheServerUrl = config.redisUrl;

        this.connect();
    }

    connect() {
        this.redisClient = redis.createClient(this.cachePort, this.cacheServerUrl)

        this.redisClient.on('connect', function () {
            console.log('Redis client connected');
        });

        this.redisClient.on('error', function (err) {
            console.log('Something went wrong ' + err);
        });
    }

    getFromCache(cacheName, callback) {
        this.redisClient.get(cacheName, callback);
    }

    setToStringCache(cacheName, cacheString) {
        this.redisClient.set(cacheName, cacheString, redis.print);
    }

    setToJSONCache(cacheName, cacheJSON) {
        var cacheString = JSON.stringify(cacheJSON);

        this.setToStringCache(cacheName, cacheString);
    }

    clearCache(cacheName) {
        this.redisClient.del(cacheName);
    }
}

module.exports = new CacheManager();

--infrastructure/configuration/configurationManager.js

const currentEnvironment = process.env.RUNNING_ENVIRONMENT || "Development";

var appConfig = require('../../configFiles/appSettings.' + currentEnvironment + '.json');

// var appConfig = require('~/configFiles/a');

module.exports = appConfig;

--infrastructure/configuration/mongoDBConnection.js

const mongoose = require('mongoose');
const config = require('./configurationManager');

function ConnectToMongoDB(connectionString) {
    var mongoDB = 'mongodb://' + config.mongoDBUrl + ':' + config.mongoDBPort + '/parameter_db';
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

--infrastructure/configuration/swaggerConfig.js

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

--configFiles/appSettings.Development.json
  
  {
    "redisPort": 6379,
    "redisUrl": "localhost",
    "mongoDBPort": 27017,
    "mongoDBUrl": "localhost"
}


--configFiles/appSettings.Docker.json
{
    "redisPort": 6379,
    "redisUrl": "localhost",
    "mongoDBPort": 27017,
    "mongoDBUrl": "localhost"
}

--routers/country.js
var express = require('express');
var router = express.Router();

const CountryController = require("../controllers/country");


/**
 * @swagger 
 * tags: 
 *   - name: country 
 *     description: country parameter service 
 */

/**
 * @swagger
 * definition:
 *   country:
 *     properties:
 *       flag:
 *         type: string
 *       name:
 *         type: string
 *       alpha2Code:
 *         type: integer
 *       alpha3Code:
 *         type: string
 *       capital:
 *         type: string
 *       region:
 *         type: string
 *       subregion:
 *         type: integer
 *       demonym:
 *         type: string 
 *       nativeName:
 *         type: string
 *       numericCode:
 *         type: string 
 */

/**
 * @swagger
 * /country:
 *  get:
 *    summary: gets countries
 *    description: Gets country list. Optionaly can use with paging
 *    tags:
 *      - country
 *    parameters:
 *      - in: query
 *        name: pageIndex
 *        type: integer
 *        required: false
 *      - in: query
 *        name: pageSize
 *        type: integer
 *        required: false
 *    produces:
 *      - application/json
 *    responses:
 *      200:
 *        description: An array of countries
 *        schema:
 *          $ref: '#/definitions/country'
 */
router.get('/', CountryController.getCountries);

/**
 * @swagger
 * /country:
 *  post:
 *    summary: post country
 *    description: Adds country to database.
 *    tags:
 *      - country
 *    parameters:
 *      - in: body
 *        name: country
 *        required: true
 *    responses:
 *      200:
 *        description: successfully added.
 */
router.post('/', CountryController.createCountry);

/**
 * @swagger
 * /country/{id}:
 *  put:
 *    summary: put country
 *    description: Updates country to database.
 *    tags:
 *      - country
 *    parameters:
 *      - in: path
 *        name: id
 *        required: true
 *      - in: body
 *        name: country
 *        required: true
 *    responses:
 *      200:
 *        description: successfully added.
 */
router.put('/:id', CountryController.updateCountry);

/**
 * @swagger
 * /country/{id}:
 *  delete:
 *    summary: delete country
 *    description: Deletes country from database.
 *    tags:
 *      - country
 *    parameters:
 *      - in: path
 *        name: id
 *        required: true
 *    responses:
 *      200:
 *        description: successfully added.
 */
router.delete('/:id', CountryController.deleteCountry);

// router.get('/:countryCode', function (req, res, next) {
//   res.send('country' + req.params.countryCode);
// });

module.exports = router;

--app.js
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
app.use(express.urlencoded({
  extended: false
}));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

//swagger utilization
const swaggerUi = require('swagger-ui-express');
const swaggerDoc = require('./infrastructure/configuration/swaggerConfig');
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerDoc));

//routers
var countryRouter = require('./routes/country');
app.use('/country', countryRouter);


app.listen(8080, function () {
  console.log('Ready on port 8080');
});

module.exports = app;

--compose
version: "3.4"
services:
  parameter.cache.mongo:
    image: redis:latest
    ports:
      - "6379:6379"
  parameter.data.mongo:
    image: mongo:latest
    ports:
      - "27017:27017"
  parameter.data.mongo.seed:
    build:
      context: .
      dockerfile: Docker.Parameter.Data.MongoDB.DataSeed.Dockerfile
    links:
      - parameter.data.mongo

--package.json
{
  "name": "parameter.api",
  "version": "0.0.0",
  "private": true,
  "scripts": {
    "start": "node ./bin/www"
  },
  "dependencies": {
    "cookie-parser": "~1.4.3",
    "debug": "~2.6.9",
    "express": "~4.16.0",
    "mongoose": "^5.4.19",
    "morgan": "~1.9.0",
    "redis": "^2.8.0",
    "swagger-jsdoc": "^3.2.7",
    "swagger-ui-express": "^4.0.2",
    "uuid": "^3.3.2"
  }
}
