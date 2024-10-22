#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Airport.gRPC/Airport.gRPC.csproj", "Airport.gRPC/"]
COPY ["Airport.Service/Airport.Service.csproj", "Airport.Service/"]
COPY ["Airport.Provider/Airport.Provider.csproj", "Airport.Provider/"]
COPY ["Common.Core/Common.Core.csproj", "Common.Core/"]
RUN dotnet restore "./Airport.gRPC/Airport.gRPC.csproj"
COPY . .
WORKDIR "/src/Airport.gRPC"
RUN dotnet build "./Airport.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Airport.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Airport.gRPC.dll"]