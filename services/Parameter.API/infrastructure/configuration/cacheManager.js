const redis = require('redis');
const config = require('./configurationManager');

class CacheManager {
  constructor() {
    this.cachePort = config.redisPort;
    this.cacheServerUrl = config.redisUrl;

    this.connect();
  }

  connect() {
    this.redisClient = redis.createClient(this.cachePort, this.cacheServerUrl);

    this.redisClient.on('connect', function() {
      console.log('Redis client connected');
    });

    this.redisClient.on('error', function(err) {
      console.log('Something went wrong ' + err);
    });
  }

  getFromCache(cacheName, callback) {
    this.redisClient.get(cacheName, callback);
  }

  setToStringCache(cacheName, cacheString) {
    this.redisClient.set(cacheName, cacheString, redis.print);
  }

  setToJSONCache(cacheName, cacheJSON) {
    var cacheString = JSON.stringify(cacheJSON);

    this.setToStringCache(cacheName, cacheString);
  }

  clearCache(cacheName) {
    this.redisClient.del(cacheName);
  }
}

module.exports = new CacheManager();
