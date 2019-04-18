var express = require('express');
var router = express.Router();

const CountryController = require('../controllers/logger');

router.get('/', CountryController.getCountries);

router.post('/', CountryController.createCountry);

router.put('/:id', CountryController.updateCountry);

router.delete('/:id', CountryController.deleteCountry);

// router.get('/:countryCode', function (req, res, next) {
//   res.send('country' + req.params.countryCode);
// });

module.exports = router;
