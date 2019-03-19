FROM microsoft/mssql-server-linux:latest

ENV ACCEPT_EULA Y
ENV MSSQL_PID Developer

WORKDIR /opt/mssql-tools/bin
COPY ./Infrastructure/DataSeeds .
CMD /opt/mssql-tools/bin/sqlcmd -S customer.data.mssql -U sa -P Strong_Passw0rd -i DataSeed.sql
