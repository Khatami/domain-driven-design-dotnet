#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["04-EndPoints/Marketplace/Marketplace.csproj", "04-EndPoints/Marketplace/"]
COPY ["02-Application/Marketplace.Application/Marketplace.Application.csproj", "02-Application/Marketplace.Application/"]
COPY ["01-Domain/Marketplace.Domain/Marketplace.Domain.csproj", "01-Domain/Marketplace.Domain/"]
COPY ["02-Application/Marketplace.Application.Contracts/Marketplace.Application.Contracts.csproj", "02-Application/Marketplace.Application.Contracts/"]
COPY ["03-Infrastructure/Marketplace.Persistence.RavenDB/Marketplace.Persistence.RavenDB.csproj", "03-Infrastructure/Marketplace.Persistence.RavenDB/"]
RUN dotnet restore "04-EndPoints/Marketplace/Marketplace.csproj"
COPY . .
WORKDIR "/src/04-EndPoints/Marketplace"
RUN dotnet build "Marketplace.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Marketplace.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Marketplace.dll"]