#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SteamMarketplace.ResourceWebApplication/SteamMarketplace.ResourceWebApplication.csproj", "SteamMarketplace.ResourceWebApplication/"]
COPY ["SteamMarketplace.HttpClients/SteamMarketplace.HttpClients.csproj", "SteamMarketplace.HttpClients/"]
COPY ["SteamMarketplace.Model.Common/SteamMarketplace.Model.Common.csproj", "SteamMarketplace.Model.Common/"]
COPY ["SteamMarketplace.HttpClients.Common/SteamMarketplace.HttpClients.Common.csproj", "SteamMarketplace.HttpClients.Common/"]
COPY ["SteamMarketplace.Model/SteamMarketplace.Model.csproj", "SteamMarketplace.Model/"]
COPY ["SteamMarketplace.Authorization.Models/SteamMarketplace.Authorization.Models.csproj", "SteamMarketplace.Authorization.Models/"]
COPY ["SteamMarketplace.Services/SteamMarketplace.Services.csproj", "SteamMarketplace.Services/"]
COPY ["SteamMarketplace.Hubs/SteamMarketplace.Hubs.csproj", "SteamMarketplace.Hubs/"]
COPY ["SteamMarketplace.Hubs.Common/SteamMarketplace.Hubs.Common.csproj", "SteamMarketplace.Hubs.Common/"]
COPY ["SteamMarketplace.Authorization.Common/SteamMarketplace.Authorization.Common.csproj", "SteamMarketplace.Authorization.Common/"]
RUN dotnet restore "SteamMarketplace.ResourceWebApplication/SteamMarketplace.ResourceWebApplication.csproj"
COPY . .
WORKDIR "/src/SteamMarketplace.ResourceWebApplication"
RUN dotnet build "SteamMarketplace.ResourceWebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SteamMarketplace.ResourceWebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SteamMarketplace.ResourceWebApplication.dll"]