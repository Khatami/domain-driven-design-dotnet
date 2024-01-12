#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["04-EndPoints/Marketplace/Marketplace.csproj", "04-EndPoints/Marketplace/"]
COPY ["02-Application/Marketplace.Application/Marketplace.Application.csproj", "02-Application/Marketplace.Application/"]
COPY ["02-Application/Marketplace.Application.Contracts/Marketplace.Application.Contracts.csproj", "02-Application/Marketplace.Application.Contracts/"]
COPY ["01-Domain/Marketplace.Domain/Marketplace.Domain.csproj", "01-Domain/Marketplace.Domain/"]
COPY ["01-Domain/Marketplace.Domain.SeedWork/Marketplace.Domain.SeedWork.csproj", "01-Domain/Marketplace.Domain.SeedWork/"]
COPY ["02-Application/Marketplace.Application.SeedWork/Marketplace.Application.SeedWork.csproj", "02-Application/Marketplace.Application.SeedWork/"]
COPY ["03-Infrastructure/BackgroundJob/Marketplace.BackgroundJob.Hangfire.MSSQL/Marketplace.BackgroundJob.Hangfire.MSSQL.csproj", "03-Infrastructure/BackgroundJob/Marketplace.BackgroundJob.Hangfire.MSSQL/"]
COPY ["03-Infrastructure/Mediator/Marketplace.Mediator.MediatR/Marketplace.Mediator.MediatR.csproj", "03-Infrastructure/Mediator/Marketplace.Mediator.MediatR/"]
COPY ["03-Infrastructure/Persistence/Marketplace.Persistence.EF/Marketplace.Persistence.EF.csproj", "03-Infrastructure/Persistence/Marketplace.Persistence.EF/"]
COPY ["03-Infrastructure/Persistence/Marketplace.Persistence.RavenDB/Marketplace.Persistence.RavenDB.csproj", "03-Infrastructure/Persistence/Marketplace.Persistence.RavenDB/"]
COPY ["03-Infrastructure/Query/Marketplace.Queries.Contracts/Marketplace.Queries.Contracts.csproj", "03-Infrastructure/Query/Marketplace.Queries.Contracts/"]
COPY ["03-Infrastructure/Query/Marketplace.Queries.EF/Marketplace.Queries.EF.csproj", "03-Infrastructure/Query/Marketplace.Queries.EF/"]
COPY ["03-Infrastructure/Query/Marketplace.Queries.RavenDB/Marketplace.Queries.RavenDB.csproj", "03-Infrastructure/Query/Marketplace.Queries.RavenDB/"]
COPY ["03-Infrastructure/Streaming/Marketplace.Streaming.EventStore/Marketplace.Streaming.EventStore.csproj", "03-Infrastructure/Streaming/Marketplace.Streaming.EventStore/"]
RUN dotnet restore "./04-EndPoints/Marketplace/./Marketplace.csproj"
COPY . .
WORKDIR "/src/04-EndPoints/Marketplace"
RUN dotnet build "./Marketplace.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Marketplace.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Marketplace.dll"]