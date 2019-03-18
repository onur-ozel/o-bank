var express = require('express');
var router = express.Router();

/**
   * @swagger
   * /:
   *   get:
   *     description: gets countries
   *     responses:
   *       200:
   *         description: countries
   */
router.get('/', function(req, res, next) {
  res.send('countries');
});

// GET /api/v1/things
router.post('/', function(req, res, next) {
  res.send('countries');
});

/**
 * This function comment is parsed by doctrine
 * @route GET /api
 * @group foo - Operations about user
 * @param {string} email.query.required - username or email - eg: user@domain
 * @param {string} password.query.required - user's password.
 * @returns {object} 200 - An array of user info
 * @returns {Error}  default - Unexpected error
 */
router.put('/', function(req, res, next) {
  res.send('countries');
});

/**
   * @swagger
   * definitions:
   *   Login:
   *     required:
   *       - username
   *       - password
   *     properties:
   *       username:
   *         type: string
   *       password:
   *         type: string
   *       path:
   *         type: string
   */

  /**
   * @swagger
   * tags:
   *   name: Users
   *   description: User management and login
   */

  /**
   * @swagger
   * tags:
   *   - name: Login
   *     description: Login
   *   - name: Accounts
   *     description: Accounts
   */

  /**
   * @swagger
   * /login:
   *   post:
   *     description: Login to the application
   *     tags: [Users, Login]
   *     produces:
   *       - application/json
   *     parameters:
   *       - $ref: '#/parameters/username'
   *       - name: password
   *         description: User's password.
   *         in: formData
   *         required: true
   *         type: string
   *     responses:
   *       200:
   *         description: login
   *         schema:
   *           type: object
   *           $ref: '#/definitions/Login'
   */
router.delete('/:countryCode', function(req, res, next) {
  res.send('countries');
});

router.get('/:countryCode', function(req, res, next) {
  res.send('country' + req.params.countryCode);
});

module.exports = router;







-------------------------------------
  
  
  
  
  const mongoose = require("mongoose");
// "flag": "https://restcountries.eu/data/ala.svg", 
// "name": "Åland Islands",
//   "alpha2Code": "AX",
//   "alpha3Code": "ALA",
//   "capital": "Mariehamn",
//   "region": "Europe",
//   "subregion": "Northern Europe",
//   "demonym": "Ålandish",
//   "nativeName": "Åland",
//     "numericCode": "248"
// }
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









-------------------------------
  
  
  
  
  
  
  var express = require('express');
var router = express.Router();

const Country = require('../models/country');

/* GET countries listing. */
router.get('', function (req, res, next) {
    Country.find().then(documents => {
        res.status(200).json({
            message: 'Posts fetched successfully!',
            posts: documents
        });
    });
});

router.post('', (req, res, next) => {
    const country = new Country({
        name: 'Afghanistan',
        alpha3Code: 'AFG',
        numericCode: 004
    });
    country.save().then(createdCountry => {
        res.status(201).json({
            message: "Post added successfully",
            post: createdCountry
        });
    });
});

module.exports = router;









-------------------------------------
  
  
  
  
  
  
  
  
    "dependencies": {
    "cookie-parser": "~1.4.3",
    "debug": "~2.6.9",
    "express": "~4.16.0",
    "mongoose": "^5.4.19",
    "morgan": "~1.9.0"
  }



FROM mongo

COPY all.json /all.json
CMD mongoimport --host parameter.data.mongo --db parameter_db --collection Country --drop --type json --file /all.json --jsonArray




version: "3.4"
services:
  parameter.data.mongo:
    image: mongo:latest
    ports:
      - "27017:27017"
  mongo-seed:
    build:
      context: ./Parameter.API
      dockerfile: Dockerfile.mongo-seed
    restart: on-failure
    depends_on:
      - parameter.data.mongo
# -e MONGO_INITDB_ROOT_USERNAME=mongoadmin \
# 	-e MONGO_INITDB_ROOT_PASSWORD=secret \




https://restcountries.eu/rest/v2/all?fields=name;numericCode;alpha3Code;alpha2Code;capital;region;subregion;nativeName;flag;demonym
