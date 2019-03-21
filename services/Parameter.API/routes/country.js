var express = require('express');
var router = express.Router();

const CountryController = require('../controllers/country');

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
