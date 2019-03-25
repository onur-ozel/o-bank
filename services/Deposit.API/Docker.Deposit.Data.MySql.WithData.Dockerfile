FROM mysql:latest

ENV MYSQL_ROOT_PASSWORD Strong_Passw0rd 
ENV MYSQL_DATABASE company

COPY ./src/main/java/com/deposit/infrastructure/dataseeds/data-seed.sql /docker-entrypoint-initdb.d/