FROM microsoft/mssql-server-linux:latest

ENV ACCEPT_EULA Y
ENV SA_PASSWORD Strong_Passw0rd
ENV MSSQL_PID Developer

WORKDIR /opt/mssql-tools/bin
COPY ./Infrastructure/DataSeeds .
RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Strong_Passw0rd -i DataSeed.sql 
