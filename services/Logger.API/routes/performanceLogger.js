var express = require('express');
var router = express.Router();

const PerformanceLoggerController = require('../controllers/performanceLogger');

router.post('/', PerformanceLoggerController.addPerformanceLog);

router.put('/', PerformanceLoggerController.updatePerformanceLog);

router.delete('/:id', PerformanceLoggerController.deletePerformanceLog);

router.get('/:id', PerformanceLoggerController.getPerformanceLogById);

router.get('/findByTopic/:topic', PerformanceLoggerController.getPerformanceLogByTopic);

module.exports = router;