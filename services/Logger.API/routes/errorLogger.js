var express = require('express');
var router = express.Router();

const ErrorLoggerController = require('../controllers/errorLogger');

router.get('/:environment', ErrorLoggerController.getErrorLogs);

module.exports = router;