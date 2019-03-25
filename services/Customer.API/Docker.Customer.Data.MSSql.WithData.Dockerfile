FROM microsoft/mssql-server-linux:latest

ENV ACCEPT_EULA Y
ENV SA_PASSWORD Strong_Passw0rd
ENV MSSQL_PID Developer

COPY ./Infrastructure/DataSeeds .
RUN ./data-seed.sh
