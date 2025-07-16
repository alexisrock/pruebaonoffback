# Imagen base para herramientas y SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /App
EXPOSE 8083

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . ./
RUN dotnet restore "pruebaonoffback.sln"
RUN dotnet build "pruebaonoffback.sln" -c $BUILD_CONFIGURATION -o /App/build

# Etapa de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api/Api.csproj" -c $BUILD_CONFIGURATION -o /App/publish /p:UseAppHost=false
# Etapa final: ejecutar migraciones e iniciar app
FROM base AS final
WORKDIR /App
COPY --from=publish /App/publish . 

# Establecer PATH para herramientas globales y ejecutar migraciones + app
ENTRYPOINT ["dotnet", "Api.dll"]

