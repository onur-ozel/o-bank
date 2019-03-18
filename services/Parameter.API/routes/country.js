var express = require('express');
var router = express.Router();



const Country = require('../models/country');

/**
   * @swagger
   * /country/:
   *   get:
   *     description: gets countries
   *     responses:
   *       200:
   *         description: countries
   */
router.get('/', function (req, res, next) {
    Country.find().then(documents => {
        res.status(200).json({
            message: 'Posts fetched successfully!',
            posts: documents
        });
    });
});


// router.get('/', function(req, res, next) {
//   res.send('countries');
// });

router.post('/', (req, res, next) => {
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

// /**
//    * @swagger
//    * definitions:
//    *   Login:
//    *     required:
//    *       - username
//    *       - password
//    *     properties:
//    *       username:
//    *         type: string
//    *       password:
//    *         type: string
//    *       path:
//    *         type: string
//    */

//   /**
//    * @swagger
//    * tags:
//    *   name: Users
//    *   description: User management and login
//    */

//   /**
//    * @swagger
//    * tags:
//    *   - name: Login
//    *     description: Login
//    *   - name: Accounts
//    *     description: Accounts
//    */

//   /**
//    * @swagger
//    * /login:
//    *   post:
//    *     description: Login to the application
//    *     tags: [Users, Login]
//    *     produces:
//    *       - application/json
//    *     parameters:
//    *       - $ref: '#/parameters/username'
//    *       - name: password
//    *         description: User's password.
//    *         in: formData
//    *         required: true
//    *         type: string
//    *     responses:
//    *       200:
//    *         description: login
//    *         schema:
//    *           type: object
//    *           $ref: '#/definitions/Login'
//    */
router.delete('/:countryCode', function(req, res, next) {
  res.send('countries');
});

router.get('/:countryCode', function(req, res, next) {
  res.send('country' + req.params.countryCode);
});

module.exports = router;