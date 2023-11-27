#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/mssql/server:2019-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Password123
ENV MSSQL_PID=Standard
ENV MSSQL_TCP_PORT=1433

WORKDIR /src
COPY init.sql ./init.sql
EXPOSE 1433
RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" &&  /opt/mssql-tools/bin/sqlcmd -S127.0.0.1 -USA -PPassword123 -i init.sql