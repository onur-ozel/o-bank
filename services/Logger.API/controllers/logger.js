// const Country = require('../models/country');
// const cacheManager = require('../infrastructure/configuration/cacheManager');

// var cacheName = 'Parameters_Countries';

// exports.getCountries = (req, res, next) => {
//   var returnObject;

//   if (req.query.pageIndex && req.query.pageSize) {
//     getDataWithPaging(req.query.pageIndex, req.query.pageSize, res);
//   } else {
//     cacheManager.getFromCache(cacheName, (error, result) => {
//       returnObject = result;

//       if (returnObject == null) {
//         Country.find().then(result => {
//           returnObject = result;

//           cacheManager.setToJSONCache(cacheName, returnObject);

//           res.status(200).json(returnObject);
//         });
//       } else {
//         res.status(200).json(JSON.parse(returnObject));
//       }
//     });
//   }
// };

// function getDataWithPaging(pageIndex, pageSize, res) {
//   var pageIndex = parseInt(pageIndex);
//   var pageSize = parseInt(pageSize);

//   var promises = [
//     Country.find()
//       .skip(pageSize * pageIndex)
//       .limit(pageSize)
//       .exec(),
//     Country.estimatedDocumentCount().exec()
//   ];

//   Promise.all(promises).then(function(results) {
//     res.status(200).json({
//       PageIndex: pageIndex,
//       PageSize: pageSize,
//       Count: results[1],
//       Data: results[0]
//     });
//   });
// }

// exports.createCountry = (req, res, next) => {
//   const country = new Country({
//     ...req.body
//   });

//   country.save().then(createdCountry => {
//     res.status(201).json({
//       message: 'Post added successfully',
//       post: createdCountry
//     });
//   });

//   cacheManager.clearCache(cacheName);
// };

// exports.updateCountry = function(req, res, next) {
//   Country.findOneAndUpdate(
//     {
//       _id: req.params.id
//     },
//     req.body
//   ).then(result => {
//     res.status(200).json({
//       result: result
//     });
//   });

//   cacheManager.clearCache(cacheName);
// };

// exports.deleteCountry = function(req, res, next) {
//   Country.findOneAndDelete({
//     _id: req.params.id
//   }).then(result => {
//     res.status(200).json({
//       result: result
//     });
//   });

//   cacheManager.clearCache(cacheName);
// };
