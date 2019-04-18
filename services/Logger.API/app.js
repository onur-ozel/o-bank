var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

var app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(
  express.urlencoded({
    extended: false
  })
);
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));


var ExpressCassandra = require('express-cassandra');
var models = ExpressCassandra.createClient({
    clientOptions: {
        contactPoints: ['127.0.0.1'],
        protocolOptions: { port: 9042 },
        keyspace: 'mykeyspace',
        queryOptions: {consistency: ExpressCassandra.consistencies.one}
    },
    ormOptions: {
        defaultReplicationStrategy : {
            class: 'SimpleStrategy',
            replication_factor: 1
        },
        migration: 'safe',
    }
});

var MyModel = models.loadSchema('Person', {
    fields:{
        name    : "text",
        surname : "text",
        age     : "int",
        created : "timestamp"
    },
    key:["name"]
});


// MyModel or models.instance.Person can now be used as the model instance
console.log(models.instance.Person === MyModel);

// sync the schema definition with the cassandra database table
// if the schema has not changed, the callback will fire immediately
// otherwise express-cassandra will try to migrate the schema and fire the callback afterwards
MyModel.syncDB(function(err, result) {
    console.log(result);
    if (err) throw err;
    // result == true if any database schema was updated
    // result == false if no schema change was detected in your models
});

var john = new models.instance.Person({
    name: "Onur",
    surname: "Aaa",
    age: 32,
    created: Date.now()
});
john.save(function(err){
    if(err) {
        console.log(err);
        return;
    }
    console.log('Yuppiie!');
});

models.instance.Person.findOne({name: 'John'}, function(err, john){
    if(err) {
        console.log(err);
        return;
    }
    //Note that returned variable john here is an instance of your model,
    //so you can also do john.delete(), john.save() type operations on the instance.
    console.log('Found ' + john.name + ' to be ' + john.age + ' years old!');
});


models.instance.Person.find({name: 'John'}, function(err, john){
    if(err) {
        console.log(err);
        return;
    }
    //Note that returned variable john here is an instance of your model,
    //so you can also do john.delete(), john.save() type operations on the instance.
    console.log(john);
});


//swagger utilization
const swaggerUi = require('swagger-ui-express');
const yaml = require('yamljs');
const swaggerDoc = yaml.load('./resources/swagger/Logger.API.v1.yaml');
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerDoc));

//routers
// var countryRouter = require('./routes/logger');
// app.use('/country', countryRouter);

app.listen(8080, function() {
  console.log('Ready on port 8080');
});

module.exports = app;
