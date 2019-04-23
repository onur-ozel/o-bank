var express = require('express');
var router = express.Router();

const PerformanceLoggerController = require('../controllers/performanceLogger');

router.get('/:environment', PerformanceLoggerController.getPerformanceLogs);

module.exports = router;