var express = require('express');
var router = express.Router();
var redis = require('redis');

const Country = require('../models/country');
let PaginatedItemsViewModel = require('../infrastructure/viewmodels/PaginatedItemsViewModel');

var client = redis.createClient('6379', 'localhost')

client.on('connect', function () {
  console.log('Redis client connected');
});

client.on('error', function (err) {
  console.log('Something went wrong ' + err);
});

/**
 * @swagger
 * /country:
 *  get:
 *    summary: gets countries
 *    description: Gets country list with paging.
 *    parameters:
 *      - in: query
 *        name: pageIndex
 *        type: integer
 *        required: false
 *        default: 0
 *      - in: query
 *        name: pageSize
 *        type: integer
 *        required: false
 *        default: 10
 *    responses:
 *      200:
 *        description: countries
 */
router.get('/', function (req, res, next) {
  var pageIndex = parseInt(req.query.pageIndex) || 0;
  var pageSize = parseInt(req.query.pageSize) || 10;

  client.get('countries' + pageIndex + pageSize, (error, result) => {
    if (result === null) {
      var promises = [
        Country.find().skip(pageSize * pageIndex).limit(pageSize).exec(),
        Country.estimatedDocumentCount().exec()
      ];

      Promise.all(promises).then(function (results) {
        client.set
        client.set('countries' + pageIndex + pageSize,
          JSON.stringify({
            PageIndex: pageIndex,
            PageSize: pageSize,
            Count: results[1],
            Data: results[0]
          }));

        res.status(200).json({
          PageIndex: pageIndex,
          PageSize: pageSize,
          Count: results[1],
          Data: results[0]
        });
      });
    } else {
      res.status(200).json(JSON.parse(result));
    }


  });



  // var promises = [
  //   Country.find().skip(pageSize * pageIndex).limit(pageSize).exec(),
  //   Country.estimatedDocumentCount().exec()
  // ];

  // Promise.all(promises).then(function (results) {
  //   res.status(200).json({
  //     PageIndex: pageIndex,
  //     PageSize: pageSize,
  //     Count: results[1],
  //     Data: results[0]
  //   });
  // });
});


/**
 * @swagger
 * /country:
 *  post:
 *    summary: post country
 *    description: Adds country to database.
 *    parameters:
 *      - in: body
 *        name: country
 *        required: true
 *    responses:
 *      200:
 *        description: successfully added.
 */
router.post('/', (req, res, next) => {
  const country = new Country({
    ...req.body
  });

  country.save().then(createdCountry => {
    res.status(201).json({
      message: "Post added successfully",
      post: createdCountry
    });
  });
});

/**
 * @swagger
 * /country/{id}:
 *  put:
 *    summary: put country
 *    description: Updates country to database.
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
router.put('/:id', function (req, res, next) {
  Country.findOneAndUpdate({
    _id: req.params.id
  }, req.body).then((result) => {
    res.status(200).json({
      result: result
    });
  });
});

/**
 * @swagger
 * /country/{id}:
 *  delete:
 *    summary: delete country
 *    description: Deletes country from database.
 *    parameters:
 *      - in: path
 *        name: id
 *        required: true
 *    responses:
 *      200:
 *        description: successfully added.
 */
router.delete('/:id', function (req, res, next) {
  Country.findOneAndDelete({
    _id: req.params.id
  }).then((result) => {
    res.status(200).json({
      result: result
    });
  });
});

router.get('/:countryCode', function (req, res, next) {
  res.send('country' + req.params.countryCode);
});

module.exports = router;
