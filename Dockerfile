FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app

FROM microsoft/aspnetcore-build:2.0 AS builder
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./

COPY RedisManager/RedisManager.csproj RedisManager/
COPY RedisWithNetCoreSampleApp/RedisWithNetCoreSampleApp.csproj RedisWithNetCoreSampleApp/

RUN dotnet restore
COPY . .
WORKDIR /src/RedisWithNetCoreSampleApp
RUN dotnet build -c $Configuration -o /app

FROM builder AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RedisWithNetCoreSampleApp.dll"]