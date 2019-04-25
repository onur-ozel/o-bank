var express = require('express');
var router = express.Router();

const ErrorLoggerController = require('../controllers/errorLogger');

router.get('/', ErrorLoggerController.getErrorLogs);

router.post('/', ErrorLoggerController.addErrorLog);

router.put('/', ErrorLoggerController.updateErrorLog);

router.delete('/:id', ErrorLoggerController.deleteErrorLog);

router.get('/:id', ErrorLoggerController.getErrorLogById);

module.exports = router;