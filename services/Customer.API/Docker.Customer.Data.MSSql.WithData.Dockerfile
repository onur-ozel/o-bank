FROM microsoft/mssql-server-linux:latest

COPY ./Infrastructure/DataSeeds /opt/mssql-tools/bin/.
