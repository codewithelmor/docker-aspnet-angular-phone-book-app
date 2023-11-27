#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 8080
EXPOSE 8081
EXPOSE 5057
EXPOSE 7017

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_20.x | bash - && \
apt-get install -y build-essential nodejs
# End Install

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["phone-book-app.client/nuget.config", "phone-book-app.client/"]
COPY ["phone-book-app.Server/phone-book-app.Server.csproj", "phone-book-app.Server/"]
COPY ["phone-book-app.client/phone-book-app.client.esproj", "phone-book-app.client/"]
RUN dotnet restore "./phone-book-app.Server/./phone-book-app.Server.csproj"
COPY . .
WORKDIR "/src/phone-book-app.Server"
RUN dotnet build "phone-book-app.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "phone-book-app.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "phone-book-app.Server.dll"]