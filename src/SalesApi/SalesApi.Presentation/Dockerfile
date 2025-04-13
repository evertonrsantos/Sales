# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8090

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SalesApi.Domain/SalesApi.Domain.csproj", "SalesApi.Domain/"]
COPY ["SalesApi.Repository/SalesApi.Repository.csproj", "SalesApi.Repository/"]
COPY ["SalesApi.Presentation/SalesApi.Presentation.csproj", "SalesApi.Presentation/"]
COPY ["SalesApi.Application/SalesApi.Application.csproj", "SalesApi.Application/"]
COPY ["SalesApi.Infrastructure.Adapter/SalesApi.Infrastructure.Adapter.csproj", "SalesApi.Infrastructure.Adapter/"]
RUN dotnet restore "./SalesApi.Presentation/SalesApi.Presentation.csproj"
COPY . .
WORKDIR "/src/SalesApi.Presentation"
RUN dotnet build "./SalesApi.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SalesApi.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/SalesApi.Presentation/wait-for-it.sh /app/wait-for-it.sh

ENTRYPOINT ["./wait-for-it.sh", "db:5432", "--timeout=60", "--", "dotnet", "SalesApi.Presentation.dll"]