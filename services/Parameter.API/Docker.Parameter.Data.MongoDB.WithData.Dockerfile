FROM mongo:latest

COPY ./infrastructure/dataseeds/countries.json /countries.json
COPY ./infrastructure/dataseeds/data-seed.sh /docker-entrypoint-initdb.d/
