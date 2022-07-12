#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SteamMarketplace.AuthorizationWebApplication/SteamMarketplace.AuthorizationWebApplication.csproj", "SteamMarketplace.AuthorizationWebApplication/"]
COPY ["SteamMarketplace.Model.Common/SteamMarketplace.Model.Common.csproj", "SteamMarketplace.Model.Common/"]
COPY ["SteamMarketplace.Authorization.Common/SteamMarketplace.Authorization.Common.csproj", "SteamMarketplace.Authorization.Common/"]
COPY ["SteamMarketplace.Model/SteamMarketplace.Model.csproj", "SteamMarketplace.Model/"]
COPY ["SteamMarketplace.Authorization.Models/SteamMarketplace.Authorization.Models.csproj", "SteamMarketplace.Authorization.Models/"]
RUN dotnet restore "SteamMarketplace.AuthorizationWebApplication/SteamMarketplace.AuthorizationWebApplication.csproj"
COPY . .
WORKDIR "/src/SteamMarketplace.AuthorizationWebApplication"
RUN dotnet build "SteamMarketplace.AuthorizationWebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SteamMarketplace.AuthorizationWebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SteamMarketplace.AuthorizationWebApplication.dll"]