/opt/mssql/bin/sqlservr --accept-eula & sleep 10 & /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Strong_Passw0rd -i data-seed.sql
