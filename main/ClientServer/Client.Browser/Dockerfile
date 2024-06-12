#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Client/Directory.Build.props", "Client/"]
COPY ["Client.Browser/Client.Browser.csproj", "Client.Browser/"]
COPY ["Client/Client/Client.csproj", "Client/Client/"]
COPY ["Helper/Helper.csproj", "Helper/"]
RUN dotnet restore "./Client.Browser/Client.Browser.csproj"
COPY . .
WORKDIR "/src/Client.Browser"
RUN dotnet build "./Client.Browser.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Client.Browser.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.Browser.dll"]