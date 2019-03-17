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
