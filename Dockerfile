# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers

COPY src/Server/*.fsproj ./Server/
RUN dotnet restore ./Server/Server.fsproj

# copy everything else and build app
COPY src/Server/. ./Server/
WORKDIR /source/Server
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Server.dll"]