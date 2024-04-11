FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["online-store-web-api/online-store-web-api.csproj", "online-store-web-api/"]
RUN dotnet restore "./online-store-web-api/./online-store-web-api.csproj"
WORKDIR "/src/online-store-web-api"
COPY /online-store-web-api .
RUN dotnet build "./online-store-web-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./online-store-web-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certificates
COPY certificates/aspnetapp.pfx /app/certificates

ENTRYPOINT ["dotnet", "online-store-web-api.dll","--environment=Development"]
