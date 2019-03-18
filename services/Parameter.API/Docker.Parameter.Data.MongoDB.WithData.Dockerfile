FROM mongo:latest

COPY ./infrastructure/dataseeds/countries.json /countries.json
CMD mongoimport --host parameter.data.mongo --db parameter_db --collection Country --drop --type json --file /countries.json --jsonArray
